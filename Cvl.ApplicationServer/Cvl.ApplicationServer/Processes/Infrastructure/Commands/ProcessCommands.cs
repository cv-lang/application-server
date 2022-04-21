using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Base;
using Cvl.ApplicationServer.Processes.Interfaces;

namespace Cvl.ApplicationServer.Core.Processes.Commands
{
    internal class ProcessCommands
    {
        private readonly ProcessInstanceContainerCommands _processInstanceContainerCommands;
        private readonly ProcessStateDataCommands _processStateDataCommands;
        private readonly IFullSerializer _fullSerializer;
        private readonly IServiceProvider _serviceProvider;

        public ProcessCommands(ProcessInstanceContainerCommands processInstanceContainerCommands,
            ProcessStateDataCommands processStateDataCommands,
            IFullSerializer fullSerializer,
            IServiceProvider serviceProvider)
        {
            _processInstanceContainerCommands = processInstanceContainerCommands;
            _processStateDataCommands = processStateDataCommands;
            _fullSerializer = fullSerializer;
            _serviceProvider = serviceProvider;
        }

        public async Task<T> CreateProcessAsync<T>(ProcessData processData) where T : IProcess
        {
            var process = (T?)_serviceProvider.GetService(typeof(T));
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(T)}'");
            }

            var stringState = _fullSerializer.Serialize(null);

            var processInstanceContainer = await _processInstanceContainerCommands
                .CreateProcessInstanceContainer(process.GetType(), stringState);

            process.ProcessData = processData;
            process.ProcessData.ProcessInstanceContainer = processInstanceContainer;

            return process;
        }

        internal async Task SaveProcessStateAsync(IProcess process)
        {
            object? state = null;

            if(process is IStateProcess stateProcess)
            {
                state = stateProcess.GetProcessState();
            } 
            else if( process is ILongRunningProcess runningProcess)
            {
                state = ((LongRunningProcessData?)runningProcess.ProcessData)?.VirtualMachine;
            } 
            else
            {
                throw new Exception($"Unknow process state for process:{process.GetType()}");
            }

             
            var stringState = _fullSerializer.Serialize(state);
            process.ProcessData.ProcessInstanceContainer.ProcessInstanceStateData.ProcessStateFullSerialization = stringState;
            await _processStateDataCommands.UpdateAsync(process.ProcessData.ProcessInstanceContainer.ProcessInstanceStateData);
        }
    }
}
