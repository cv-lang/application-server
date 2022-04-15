using Cvl.ApplicationServer.Core.Processes.Commands;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
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
                    var result = processLongRunningProcess
                        .LongRunningProcessData.VirtualMachine.Resume<object>(externalData);
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
            var process = _processCommands.CreateProcessAsync<TProcess>().Result;

            process.ProcessData.ProcessInstanceContainer.ProcessTypeData
                .ProcessType = ProcessType.LongRunningProcess;

            var vm = new VirtualMachine.VirtualMachine();
            process.LongRunningProcessData.VirtualMachine = vm;


            var virtualMachineResult = vm.Start<object>("StartLongRunningProcess", process);
            var processStatus = AfterRunProcess(process, virtualMachineResult);
            await _processCommands.SaveProcessStateAsync(process);

            return processStatus;
        }

        protected object BeforeRunProcess(ILongRunningProcess process)
        {
            //sprawdzam czy zahibenowany proces ma jakieś dane do pocesu
            var vmParams = process.LongRunningProcessData.VirtualMachine.GetHibernateParams();
            var type = (ProcessHibernationType)vmParams[0];
            object externalData = null;
            if (type == ProcessHibernationType.WaitingForExternalData
                || type == ProcessHibernationType.WaitingForUserInterface)
            {
                var xmlExternalData = process.ProcessData.ProcessInstanceContainer
                    .ProcessExternalData.ProcessExternalDataFullSerialization;
                externalData = _fullSerializer.Deserialize<object>(xmlExternalData);
            }

            return externalData;
        }

        protected LongRunningProcessResult AfterRunProcess(ILongRunningProcess process, VirtualMachineResult<object> virtualMachineResult)
        {
            var ret = new LongRunningProcessResult()
            {
                ProcessId = process.ProcessData.ProcessId,
                ProcessNumber = process.ProcessData.ProcessNumber,
                State = LongRunningProcessState.Pending
            };

            if (virtualMachineResult.State == VirtualMachineState.Executed)
            {
                process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.Executed;
                ret.State = LongRunningProcessState.Executed;
                return ret;
            }
            else if (virtualMachineResult.State == VirtualMachineState.Hibernated)
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
                        process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.WaitingForExecution;
                        
                        ret.State= LongRunningProcessState.Pending;
                        return ret;
                    case ProcessHibernationType.WaitingForExternalData:
                        process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.WaitingForExternalData;
                        var externalOutputData = vmParams[1];

                        xml = _fullSerializer.Serialize(externalOutputData);
                        process.ProcessData.ProcessInstanceContainer
                            .ProcessExternalData.ProcessExternalDataFullSerialization = xml;

                        ret.State = LongRunningProcessState.WaitingForExternalData;
                        ret.Result = externalOutputData;
                        return ret;
                    case ProcessHibernationType.WaitingForUserInterface:
                        process.ProcessData.ProcessInstanceContainer.ThreadData.MainThreadState = Threading.ThreadState.WaitingForUserInterface;
                        xml = _fullSerializer.Serialize(vmParams[1]);
                        process.ProcessData.ProcessInstanceContainer
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
