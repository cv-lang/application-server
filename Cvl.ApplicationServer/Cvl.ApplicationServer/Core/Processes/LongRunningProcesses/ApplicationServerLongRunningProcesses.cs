using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Commands;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.LongRunningProcesses;
using Cvl.ApplicationServer.Processes.Workers;

namespace Cvl.ApplicationServer.Core.Processes.LongRunningProcesses
{
    public class LongRunningProcessProxy<T> : IDisposable
        where T : IProcess
    {
        private readonly ApplicationServerLongRunningProcesses _applicationServerProcesses;

        internal LongRunningProcessProxy(T process, ApplicationServerLongRunningProcesses applicationServerProcesses)
        {
            _applicationServerProcesses = applicationServerProcesses;
            Process = process;
        }

        public T Process { get; private set; }

        public void Dispose()
        {
            _applicationServerProcesses.SaveProcessAsync(Process)
                .Wait();
        }
    }

    internal class ApplicationServerLongRunningProcesses : IApplicationServerLongRunningProcesses
    {
        private readonly ProcessQueries _processQueries;
        private readonly IProcessesWorker _worker;
        private readonly ProcessCommands _processCommands;

        public ApplicationServerLongRunningProcesses(ProcessQueries processQueries, IProcessesWorker worker,
            ProcessCommands processCommands)
        {
            _processQueries = processQueries;
            _worker = worker;
            _processCommands = processCommands;
        }

        public async Task<LongRunningProcessProxy<T>> OpenProcessProxyAsync<T>(string processNumber) where T : IProcess
        {
            var process = await _processQueries.LoadProcessAsync<T>(processNumber);
            var proxy = new LongRunningProcessProxy<T>(process, this);
            return proxy;
        }

        public async Task<LongRunningProcessStatus> StartLongRunningProcessAsync<T>(object inputParameter) where T : ILongRunningProcess
        {
            return await _worker.StartLongRunningProcessAsync<T>(inputParameter);
        }

        internal async Task SaveProcessAsync(IProcess process)
        {
            await _processCommands.SaveProcessStateAsync(process);
        }
    }
}
