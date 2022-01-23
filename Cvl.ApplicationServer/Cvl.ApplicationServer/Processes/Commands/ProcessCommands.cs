using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Old.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces2;

namespace Cvl.ApplicationServer.Processes.Commands
{
    public class ProcessCommands
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

        public async Task<T> CreateProcessAsync<T>() where T : IProcess
        {
            var process = (T)_serviceProvider.GetService(typeof(T));
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(T)}'");
            }

            var processInstanceContainer = await _processInstanceContainerCommands
                .CreateProcessInstanceContainer(process.GetType());

            process.ProcessData = new ProcessData();
            process.ProcessData.ProcessInstanceContainer = processInstanceContainer;

            return process;
        }

        internal async Task SaveProcessStateAsync(IProcess process)
        {
            var state = process.GetProcessState();
            var stringState = _fullSerializer.Serialize(state);
            process.ProcessData.ProcessInstanceContainer.ProcessInstanceStateData.ProcessStateFullSerialization = stringState;
            await _processStateDataCommands.UpdateAsync(process.ProcessData.ProcessInstanceContainer.ProcessInstanceStateData);
        }
    }
}
