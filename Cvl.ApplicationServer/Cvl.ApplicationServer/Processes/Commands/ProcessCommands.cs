using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Interfaces;

namespace Cvl.ApplicationServer.Processes.Commands
{
    public class ProcessCommands
    {
        private readonly ProcessInstanceContainerCommands _processInstanceContainerCommands;
        private readonly IServiceProvider _serviceProvider;

        public ProcessCommands(ProcessInstanceContainerCommands processInstanceContainerCommands,
            IServiceProvider serviceProvider)
        {
            _processInstanceContainerCommands = processInstanceContainerCommands;
            _serviceProvider = serviceProvider;
        }

        public async Task<T> CreateProcessAsync<T>() where T : class, IProcess
        {
            var process = _serviceProvider.GetService(typeof(T)) as T;
            if (process == null)
            {
                throw new ArgumentException($"Could not create a process '{typeof(T)}'");
            }

            var processInstanceContainer = await _processInstanceContainerCommands
                .CreateProcessInstanceContainer(process.GetType());

            process.ProcessId = processInstanceContainer.Id;
            process.ProcessNumber = processInstanceContainer.ProcessNumber;

            return process;
        }
    }
}
