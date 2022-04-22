using Cvl.ApplicationServer.Processes.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.StepProcesses.Core
{
    internal class ProcessManagerFactory : IProcessManagerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ProcessManagerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }        
        public IProcessManager CreateProcessManager(IProcess process)
        {
            var processManager = _serviceProvider.GetRequiredService<IProcessManager>();
            processManager.Process = process;
            return processManager;
        }
    }
}
