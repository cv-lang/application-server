using Cvl.ApplicationServer.Processes.Core.Base;
using Cvl.ApplicationServer.Processes.StepBaseProcesses.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Cvl.ApplicationServer.Processes.StepBaseProcesses.Factories
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
