using Cvl.ApplicationServer.Core.Processes.LongRunningProcesses;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Services
{
    internal class ProcessProxyService : IProcessProxyService
    {
        private readonly ProcessQueries _processQueries;
        private readonly LongRunningProcessesService _longRunningProcessesService;

        public ProcessProxyService(ProcessQueries processQueries, LongRunningProcessesService longRunningProcessesService)
        {
            _processQueries = processQueries;
            _longRunningProcessesService = longRunningProcessesService;
        }

        public async Task<LongRunningProcessProxy<T>> OpenProcessProxyAsync<T>(string processNumber) where T : IProcess
        {
            var process = await _processQueries.LoadProcessAsync<T>(processNumber);
            var proxy = new LongRunningProcessProxy<T>(process, _longRunningProcessesService);
            return proxy;
        }
    }
}
