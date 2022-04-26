using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Core.Base;
using Cvl.ApplicationServer.Processes.Core.Commands;
using Cvl.ApplicationServer.Processes.Core.Model.OwnedClasses;
using Cvl.ApplicationServer.Processes.Core.Queries;
using Cvl.ApplicationServer.Processes.LongRunningProcesses.Core;
using Cvl.VirtualMachine;
using Cvl.VirtualMachine.Core;
using ThreadState = Cvl.ApplicationServer.Processes.Core.Threading.ThreadState;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Workers
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

        public async Task<TimeSpan> RunProcessesAsync()
        {
            var processesNumbers = await _processQueries.GetWaitingForExecutionProcessesNumbersAsync(ProcessType.LongRunningProcess);

            foreach (var processNumber in processesNumbers)
            {
                var processLongRunningProcess = await _processQueries.LoadProcessAsync<ILongRunningProcess>(processNumber);
                
                var externalData = BeforeRunProcess(processLongRunningProcess);
                var processData = (LongRunningProcessData)processLongRunningProcess.ProcessData;
                var result = processData.VirtualMachine.Resume<object>(externalData);
                AfterRunProcess(processLongRunningProcess, result);
                
                //zapisuje stan procesu
                await _processCommands.SaveProcessStateAsync(processLongRunningProcess);
            }

            return TimeSpan.FromSeconds(30);
        }

        public async Task RunLoopProcessesAsync()
        {
            while (true)
            {
                var delay = await RunProcessesAsync();
                await Task.Delay(delay);
            }
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
                processData.ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.Executed;
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
                        processData.ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.WaitingForExecution;
                        
                        ret.State= LongRunningProcessState.Pending;
                        return ret;
                    case ProcessHibernationType.WaitingForExternalData:
                        processData.ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.WaitingForExternalData;
                        var externalOutputData = vmParams[1];

                        xml = _fullSerializer.Serialize(externalOutputData);
                        processData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessExternalDataFullSerialization = xml;

                        ret.State = LongRunningProcessState.WaitingForExternalData;
                        ret.Result = externalOutputData;
                        return ret;
                    case ProcessHibernationType.WaitingForUserInterface:
                        processData.ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.WaitingForUserInterface;
                        xml = _fullSerializer.Serialize(vmParams[1]);
                        processData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessExternalDataFullSerialization = xml;
                        ret.State = LongRunningProcessState.WaitingForUserInterface;
                        ret.Result = vmParams[1];
                        return ret;
                    case ProcessHibernationType.Executed:
                        processData.ProcessInstanceContainer.ThreadData.MainThreadState = ThreadState.Executed;
                        processData.ProcessInstanceContainer.StatusName = vmParams[1].ToString();
                        xml = _fullSerializer.Serialize(vmParams[2]); //result
                        processData.ProcessInstanceContainer
                            .ProcessInstanceStateData.ProcessResultFullSerialization = xml;
                        xml = _fullSerializer.Serialize(vmParams[3]);
                        processData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessExternalDataFullSerialization = xml;//view
                        ret.State = LongRunningProcessState.Executed;
                        return ret;
                }
            }

            return ret;
        }
    }
}
