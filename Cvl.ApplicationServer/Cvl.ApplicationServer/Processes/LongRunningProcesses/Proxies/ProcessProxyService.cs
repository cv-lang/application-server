using Cvl.ApplicationServer.Processes.Core.Base;
using Cvl.ApplicationServer.Processes.Core.Queries;
using Cvl.ApplicationServer.Processes.LongRunningProcesses.Services;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Proxies
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
