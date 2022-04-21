using Cvl.ApplicationServer.Core.Processes.Commands;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.LongRunningProcesses;
using Cvl.VirtualMachine;
using Cvl.VirtualMachine.Core;

namespace Cvl.ApplicationServer.Core.Processes.LongRunningProcesses
{
    internal class LongRunningProcessWorker : ILongRunningProcessWorker
    {
        private readonly ProcessCommands _processCommands;
        private readonly ProcessQueries _processQueries;
        private readonly IFullSerializer _fullSerializer;

        public LongRunningProcessWorker(ProcessCommands processCommands, ProcessQueries processQueries,
            IFullSerializer fullSerializer)
        {
            _processCommands = processCommands;
            _processQueries = processQueries;
            _fullSerializer = fullSerializer;
        }

        public async Task<int> RunProcessesAsync()
        {
            var processesNumbers = await _processQueries.GetWaitingForExecutionProcessesNumbersAsync();

            foreach (var processNumber in processesNumbers)
            {
                var process = await _processQueries.LoadProcessAsync<IProcess>(processNumber);

                if (process is ILongRunningProcess processLongRunningProcess)
                {
                    var externalData = BeforeRunProcess(processLongRunningProcess);
                    var processData = (LongRunningProcessData)processLongRunningProcess.ProcessData;
                    var result = processData.VirtualMachine.Resume<object>(externalData);
                    AfterRunProcess(processLongRunningProcess, result);
                }               

                //zapisuje stan procesu
                await _processCommands.SaveProcessStateAsync(process);
            }

            return processesNumbers.Count;
        }       

        public async Task<LongRunningProcessResult> StartLongRunningProcessAsync<TProcess>(object inputParameter) 
            where TProcess : ILongRunningProcess
        {
            var processData = new LongRunningProcessData();
            var process = await _processCommands.CreateProcessAsync<TProcess>(processData);

            processData.ProcessInstanceContainer.ProcessTypeData
                .ProcessType = ProcessType.LongRunningProcess;

            var vm = new VirtualMachine.VirtualMachine();
            processData.VirtualMachine = vm;


            var virtualMachineResult = vm.Start<object>(nameof(ILongRunningProcess.StartProcess), process);
            var processStatus = AfterRunProcess(process, virtualMachineResult);
            await _processCommands.SaveProcessStateAsync(process);

            return processStatus;
        }

        protected object? BeforeRunProcess(ILongRunningProcess process)
        {
            //sprawdzam czy zahibenowany proces ma jakieś dane do pocesu
            var processData = (LongRunningProcessData)process.ProcessData;
            var vmParams = processData.VirtualMachine.GetHibernateParams();
            var type = (ProcessHibernationType)vmParams[0];
            object externalData = null;
            if (type == ProcessHibernationType.WaitingForExternalData
                || type == ProcessHibernationType.WaitingForUserInterface)
            {
                var xmlExternalData = processData.ProcessInstanceContainer
                    .ProcessExternalData.ProcessExternalDataFullSerialization;
                externalData = _fullSerializer.Deserialize<object?>(xmlExternalData);
            }

            return externalData;
        }

        protected LongRunningProcessResult AfterRunProcess(ILongRunningProcess process, VirtualMachineResult<object> virtualMachineResult)
        {
            var processData = (LongRunningProcessData)process.ProcessData;

            var ret = new LongRunningProcessResult()
            {
                ProcessId = processData.ProcessId,
                ProcessNumber = processData.ProcessNumber,
                State = LongRunningProcessState.Pending
            };

            if (virtualMachineResult.State == VirtualMachineState.Executed)
            {
                processData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.Executed;
                ret.State = LongRunningProcessState.Executed;
                return ret;
            }
            else if (virtualMachineResult.State == VirtualMachineState.Hibernated)
            {
                var vmParams = processData.VirtualMachine.GetHibernateParams();
                var type = (ProcessHibernationType)vmParams[0];
                string xml = "";

                switch (type)
                {
                    case ProcessHibernationType.DelayOfProcessExecution:
                        var nextExecutionDate = (DateTime)vmParams[1];
                        processData.ProcessInstanceContainer.ThreadData.NextExecutionDate =
                            nextExecutionDate;
                        processData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.WaitingForExecution;
                        
                        ret.State= LongRunningProcessState.Pending;
                        return ret;
                    case ProcessHibernationType.WaitingForExternalData:
                        processData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.WaitingForExternalData;
                        var externalOutputData = vmParams[1];

                        xml = _fullSerializer.Serialize(externalOutputData);
                        processData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessExternalDataFullSerialization = xml;

                        ret.State = LongRunningProcessState.WaitingForExternalData;
                        ret.Result = externalOutputData;
                        return ret;
                    case ProcessHibernationType.WaitingForUserInterface:
                        processData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.WaitingForUserInterface;
                        xml = _fullSerializer.Serialize(vmParams[1]);
                        processData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessExternalDataFullSerialization = xml;
                        ret.State = LongRunningProcessState.WaitingForUserInterface;
                        ret.Result = vmParams[1];
                        return ret;
                }
            }

            return ret;
        }
    }
}
