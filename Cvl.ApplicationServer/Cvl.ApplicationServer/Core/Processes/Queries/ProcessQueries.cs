using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;

namespace Cvl.ApplicationServer.Core.Processes.Queries
{
    public class ProcessQueries
    {
        private readonly ProcessInstanceContainerQueries _processInstanceContainerQueries;
        private readonly IServiceProvider _serviceProvider;
        private readonly IFullSerializer _fullSerializer;

        public ProcessQueries(ProcessInstanceContainerQueries processInstanceContainerQueries,
            IServiceProvider serviceProvider, IFullSerializer fullSerializer)
        {
            _processInstanceContainerQueries = processInstanceContainerQueries;
            _serviceProvider = serviceProvider;
            _fullSerializer = fullSerializer;
        }

        public async Task<TProcesInterface> LoadProcessAsync<TProcesInterface>(string processNumber)
            where TProcesInterface : IProcess
        {
            var processInstanceContainer = await _processInstanceContainerQueries
                .GetProcessInstanceContainerByProcessNumber(processNumber);

            if (processInstanceContainer.ProcessTypeData.ProcessType == ProcessType.StepBaseProcess)
            {
                var processType = Type.GetType(processInstanceContainer.ProcessTypeData.ProcessTypeFullName);

                var process = (TProcesInterface)_serviceProvider.GetService(processType);
                if (process == null)
                {
                    throw new ArgumentException($"Could not create a process '{typeof(TProcesInterface)}'");
                }

                process.ProcessData = new ProcessData() { ProcessInstanceContainer = processInstanceContainer };

                var state = _fullSerializer.Deserialize<object>(processInstanceContainer.ProcessInstanceStateData.ProcessStateFullSerialization);
                process.LoadProcessState(state);

                return process;
            }
            else if(processInstanceContainer.ProcessTypeData.ProcessType == ProcessType.LongRunningProcess)
            {
                var vm = _fullSerializer.Deserialize<VirtualMachine.VirtualMachine>(processInstanceContainer.ProcessInstanceStateData.ProcessStateFullSerialization);
                var process = (ILongRunningProcess)vm.Instance;
                process.ProcessData = new ProcessData() { ProcessInstanceContainer = processInstanceContainer };
                process.LongRunningProcessData = new LongRunningProcessData() { VirtualMachine = vm };

                return (TProcesInterface)process;
            }

            throw new NotSupportedException(
                $"Process type not suported: {processInstanceContainer.ProcessTypeData.ProcessType}");
        }

        internal async Task<List<string>> GetWaitingForExecutionProcessesNumbersAsync()
        {
            return await _processInstanceContainerQueries.GetWaitingForExecutionProcessesNumbersAsync();
        }

        internal IQueryable<ProcessInstanceContainer> GetAllProcessInstanceContainers()
        {
            return _processInstanceContainerQueries.GetAll();
        }
    }
}
