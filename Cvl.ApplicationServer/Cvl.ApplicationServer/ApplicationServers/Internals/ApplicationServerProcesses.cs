using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Interfaces;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Migrations;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Commands;
using Cvl.ApplicationServer.Processes.Dtos;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces2;
using Cvl.ApplicationServer.Processes.Model.OwnedClasses;
using Cvl.ApplicationServer.Processes.Queries;
using Cvl.ApplicationServer.Processes.UI;
using Cvl.VirtualMachine;
using Cvl.VirtualMachine.Core;
using Cvl.VirtualMachine.Core.Variables.Values;
using Microsoft.EntityFrameworkCore;
using ThreadState = Cvl.ApplicationServer.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.ApplicationServers.Internals
{
    public class ApplicationServerProcesses : Cvl.ApplicationServer.Core.Interfaces.IApplicationServerProcesses
    {
        private readonly ProcessCommands _processCommands;
        private readonly ProcessQueries _processQueries;
        private readonly IFullSerializer _fullSerializer;
        private readonly ProcessExternalDataCommands _processExternalDataCommands;
        private readonly ProcessActivityQueries _processActivityQueries;
        private readonly ProcessStepQueries _processStepQueries;

        public ApplicationServerProcesses(ProcessCommands processCommands, ProcessQueries processQueries,
            IFullSerializer fullSerializer,
            ProcessExternalDataCommands processExternalDataCommands,
            ProcessActivityQueries processActivityQueries,
            ProcessStepQueries processStepQueries
            )
        {
            _processCommands = processCommands;
            _processQueries = processQueries;
            _fullSerializer = fullSerializer;
            _processExternalDataCommands = processExternalDataCommands;
            _processActivityQueries = processActivityQueries;
            _processStepQueries = processStepQueries;
        }

        #region Simple process

        public T CreateProcess<T>() where T : IProcess
        {
            return _processCommands.CreateProcessAsync<T>().Result;
        }

        public IProcess LoadProcess(string processNumber)
        {
            return (BaseProcess)_processQueries.LoadProcessAsync<BaseProcess>(processNumber).Result;
        }

        public T LoadProcess<T>(string processNumber) where T : IProcess
        {
            return (T)LoadProcess(processNumber);
        }

        #endregion


        public T StartProcess<T>() where T : IProcess
        {
            var process = CreateProcess<T>();

            return process;
        }


        public ProcessStatus StartLongRunningProcess<T>(object inputParameter) where T : ILongRunningProcess
        {
            

            var process = CreateProcess<T>();

            process.ProcessData.ProcessInstanceContainer.ProcessTypeData
                .ProcessType = ProcessType.LongRunningProcess;

            var vm = new VirtualMachine.VirtualMachine();
            process.LongRunningProcessData.VirtualMachine = vm;

            
            var result = vm.Start<object>("Start", process);
            AfterRunProcess(process, result);
            SaveProcess(process);

            if (vm.Thread.Status == VirtualMachineState.Hibernated)
            {
                var processStatus = new ProcessStatus();
                processStatus.Status = ProcessExecutionStaus.Pending;
                processStatus.ProcessId = process.ProcessData.ProcessId;
                processStatus.ProcessNumber = process.ProcessData.ProcessNumber;

                return processStatus;
            }
            else
            {
                throw new Exception("Process end immediately. It should use hibernation for long running process");
            }
        }


        public void SaveProcess(IProcess process)
        {
            _processCommands.SaveProcessStateAsync(process).Wait();
        }

        public int RunProcesses()
        {
            var processesNumbers = _processQueries.GetWaitingForExecutionProcessesNumbersAsync().Result;

            foreach (var processNumber in processesNumbers)
            {
                var process = LoadProcess(processNumber);

                if (process is ILongRunningProcess processLongRunningProcess)
                {
                    var externalData = BeforeRunProcess(processLongRunningProcess);
                    var result = processLongRunningProcess.ResumeLongRunningProcess(externalData);
                    AfterRunProcess(processLongRunningProcess, result);
                }
                else
                {
                    process.JobEntry();
                }

                //zapisuje stan procesu
                SaveProcess(process);
            }

            return processesNumbers.Count;
        }

        protected object BeforeRunProcess(ILongRunningProcess process)
        {
            //sprawdzam czy zahibenowany proces ma jakieś dane do pocesu
            var vmParams = process.LongRunningProcessData.VirtualMachine.GetHibernateParams();
            var type = (ProcessHibernationType)vmParams[0];
            object externalData = null;
            if (type == ProcessHibernationType.WaitForExternalData
                || type == ProcessHibernationType.WaitingForUserInterface)
            {
                var xmlExternalData = process.ProcessData.ProcessInstanceContainer
                    .ProcessExternalData.ExternalInputDataFullSerialization;
                externalData = _fullSerializer.Deserialize<object>(xmlExternalData);
            }

            return externalData;
        }

        protected void AfterRunProcess(ILongRunningProcess process, VirtualMachineResult<object> result)
        {
            if (result.State == VirtualMachineState.Executed)
            {
                process.ProcessData.ThreadStatus = ThreadState.Executed;
            }
            else if (result.State == VirtualMachineState.Hibernated)
            {
                var vmParams = process.LongRunningProcessData.VirtualMachine.GetHibernateParams();
                var type = (ProcessHibernationType)vmParams[0];
                string xml = "";

                switch (type)
                {
                    case ProcessHibernationType.DelayOfProcessExecution:
                        var nextExecutionDate = (DateTime)vmParams[1];
                        process.ProcessData.ProcessInstanceContainer.ThreadData.NextExecutionDate =
                            nextExecutionDate;
                        process.ProcessData.ThreadStatus = ThreadState.WaitingForExecution;
                        break;
                    case ProcessHibernationType.WaitForExternalData:
                        process.ProcessData.ThreadStatus = ThreadState.WaitForExternalData;
                        var externalOutputData = vmParams[1];

                        xml = _fullSerializer.Serialize(externalOutputData);
                        process.ProcessData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessOutputDataFullSerialization = xml;
                        break;
                    case ProcessHibernationType.WaitingForUserInterface:
                        process.ProcessData.ThreadStatus = ThreadState.WaitingForUserInterface;
                        xml = _fullSerializer.Serialize(vmParams[1]);
                        process.ProcessData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessOutputDataFullSerialization = xml;
                        break;
                }
            }
        }

        public object GetExternalDataOutput(string processNumber)
        {
            var process = LoadProcess(processNumber);
            var externalDataXml = process.ProcessData.ProcessInstanceContainer
                .ProcessExternalData.ProcessOutputDataFullSerialization;
            var externalData = _fullSerializer.Deserialize<object>(externalDataXml);

            return externalData;
        }

        public object GetExternalDataInput(string processNumber)
        {
            var process = LoadProcess(processNumber);
            var externalDataXml = process.ProcessData.ProcessInstanceContainer
                .ProcessExternalData.ExternalInputDataFullSerialization;
            var externalData = _fullSerializer.Deserialize<object>(externalDataXml);

            return externalData;
        }
        public void SetExternalDataInput(string processNumber, object externalData)
        {
            var process = LoadProcess(processNumber);

            var xml = _fullSerializer.Serialize(externalData);
            process.ProcessData.ProcessInstanceContainer
                .ProcessExternalData.ExternalInputDataFullSerialization = xml;
            process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.WaitingForExecution;
            SaveProcess(process);
        }

        public View GetViewData(string processNumber)
        {
            return (View)GetExternalDataOutput(processNumber);
        }

        public void SetViewResponse(string processNumbr, ViewResponse viewResponse)
        {
            SetExternalDataInput(processNumbr, viewResponse);
        }

        public IQueryable<ProcessInstanceContainer> GetAllProcesses()
        {
            return _processQueries.GetAllProcessInstanceContainers();
        }

        public IQueryable<ProcessListItemDto> GetAllProcessesDto()
        {
            var list = _processQueries.GetAllProcessInstanceContainers();

            var listDto = list.Include(x => x.ProcessDiagnosticData)
                .Select(x => new ProcessListItemDto(x));
            return listDto;
        }

        public IQueryable<ProcessActivity> GetProcessActivities(long processId)
        {
            return _processActivityQueries.GetAll();
        }

        public IQueryable<ProcessStepHistory> GetProcessSteps(long processId)
        {
            return _processStepQueries.GetAll();
        }
    }
}
