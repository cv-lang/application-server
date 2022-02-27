using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Commands;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Workers;
using Cvl.VirtualMachine;
using Cvl.VirtualMachine.Core;
using ThreadState = Cvl.ApplicationServer.Core.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.Core.Processes.Workers
{
    internal class ProcessesWorker : IProcessesWorker
    {
        private readonly ProcessCommands _processCommands;
        private readonly ProcessQueries _processQueries;
        private readonly IFullSerializer _fullSerializer;

        public ProcessesWorker(ProcessCommands processCommands, ProcessQueries processQueries,
            IFullSerializer fullSerializer)
        {
            _processCommands = processCommands;
            _processQueries = processQueries;
            _fullSerializer = fullSerializer;
        }

        private IProcess LoadProcess(string processNumber)
        {
            return _processQueries.LoadProcessAsync<IProcess>(processNumber).Result;
        }
        private void SaveProcess(IProcess process)
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

        public ProcessStatus StartLongRunningProcess<T>(object inputParameter) where T : ILongRunningProcess
        {
            var process = _processCommands.CreateProcessAsync<T>().Result;

            process.ProcessData.ProcessInstanceContainer.ProcessTypeData
                .ProcessType = ProcessType.LongRunningProcess;

            var vm = new VirtualMachine.VirtualMachine();
            process.ProcessData.LongRunningProcessData.VirtualMachine = vm;


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

        protected object BeforeRunProcess(ILongRunningProcess process)
        {
            //sprawdzam czy zahibenowany proces ma jakieś dane do pocesu
            var vmParams = process.ProcessData.LongRunningProcessData.VirtualMachine.GetHibernateParams();
            var type = (ProcessHibernationType)vmParams[0];
            object externalData = null;
            if (type == ProcessHibernationType.WaitForExternalData
                || type == ProcessHibernationType.WaitingForUserInterface)
            {
                var xmlExternalData = process.ProcessData.ProcessInstanceContainer
                    .ProcessExternalData.ProcessExternalDataFullSerialization;
                externalData = _fullSerializer.Deserialize<object>(xmlExternalData);
            }

            return externalData;
        }

        protected void AfterRunProcess(ILongRunningProcess process, VirtualMachineResult<object> result)
        {
            if (result.State == VirtualMachineState.Executed)
            {
                process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.Executed;
            }
            else if (result.State == VirtualMachineState.Hibernated)
            {
                var vmParams = process.ProcessData.LongRunningProcessData.VirtualMachine.GetHibernateParams();
                var type = (ProcessHibernationType)vmParams[0];
                string xml = "";

                switch (type)
                {
                    case ProcessHibernationType.DelayOfProcessExecution:
                        var nextExecutionDate = (DateTime)vmParams[1];
                        process.ProcessData.ProcessInstanceContainer.ThreadData.NextExecutionDate =
                            nextExecutionDate;
                        process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.WaitingForExecution;
                        break;
                    case ProcessHibernationType.WaitForExternalData:
                        process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.WaitForExternalData;
                        var externalOutputData = vmParams[1];

                        xml = _fullSerializer.Serialize(externalOutputData);
                        process.ProcessData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessExternalDataFullSerialization = xml;
                        break;
                    case ProcessHibernationType.WaitingForUserInterface:
                        process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.WaitingForUserInterface;
                        xml = _fullSerializer.Serialize(vmParams[1]);
                        process.ProcessData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessExternalDataFullSerialization = xml;
                        break;
                }
            }
        }
    }
}
