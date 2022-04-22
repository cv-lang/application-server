using Cvl.ApplicationServer.Processes.LongRunningProcesses.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Factories
{
    internal class LongRunningProcessManagerFactory : ILongRunningProcessManagerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public LongRunningProcessManagerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ILongRunningProcessManager CreateProcessManager(ILongRunningProcess process)
        {
            var processManager = _serviceProvider.GetRequiredService<ILongRunningProcessManager>();
            processManager.Process = process;
            return processManager;
        }
    }
}
