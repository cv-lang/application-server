using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Core
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
