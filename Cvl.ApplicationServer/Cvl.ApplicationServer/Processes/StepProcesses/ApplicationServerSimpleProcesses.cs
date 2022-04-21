using Cvl.ApplicationServer.Core.Processes;
using Cvl.ApplicationServer.Core.Processes.Commands;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Processes.SimpleProcesses;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Base;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.LongRunningProcesses;
using Cvl.VirtualMachine;
using Cvl.VirtualMachine.Core;
using Microsoft.EntityFrameworkCore;
using ThreadState = Cvl.ApplicationServer.Core.Processes.Threading.ThreadState;

namespace Cvl.ApplicationServer.Core.ApplicationServers.Internals
{
    public class SimpleProcessProxy<T> : IDisposable
        where T : IProcess
    {
        private readonly ApplicationServerSimpleProcesses _applicationServerProcesses;

        internal SimpleProcessProxy(T process, ApplicationServerSimpleProcesses applicationServerProcesses)
        {
            _applicationServerProcesses = applicationServerProcesses;
            Process = process;
        }

        public T Process { get; private set; }

        public void Dispose()
        {
            _applicationServerProcesses.SaveProcessAsync(Process).Wait();
        }
    }

    internal class ApplicationServerSimpleProcesses : IApplicationServerSimpleProcesses
    {
        private readonly ILongRunningProcessWorker _worker;
        private readonly ProcessCommands _processCommands;
        private readonly ProcessQueries _processQueries;
        private readonly IFullSerializer _fullSerializer;
        private readonly ProcessExternalDataCommands _processExternalDataCommands;
        private readonly ProcessActivityQueries _processActivityQueries;
        private readonly ProcessStepQueries _processStepQueries;

        public ApplicationServerSimpleProcesses(ProcessCommands processCommands, ProcessQueries processQueries,
            IFullSerializer fullSerializer,
            ProcessExternalDataCommands processExternalDataCommands,
            ProcessActivityQueries processActivityQueries,
            ProcessStepQueries processStepQueries,
            ILongRunningProcessWorker worker)
        {
            _worker = worker;
            _processCommands = processCommands;
            _processQueries = processQueries;
            _fullSerializer = fullSerializer;
            _processExternalDataCommands = processExternalDataCommands;
            _processActivityQueries = processActivityQueries;
            _processStepQueries = processStepQueries;
        }

        
        public async Task<SimpleProcessStatus> StartProcessAsync<T>() where T : IProcess
        {
            var process = await _processCommands.CreateProcessAsync<T>(new ProcessData());
            return new SimpleProcessStatus(process.ProcessData.ProcessNumber);
        }

        public async Task<SimpleProcessProxy<T>> OpenProcessProxyAsync<T>(string processNumber) where T : IProcess
        {
            var process = await _processQueries.LoadProcessAsync<T>(processNumber);
            var proxy = new SimpleProcessProxy<T>(process, this);
            return proxy;
        }

        internal async Task SaveProcessAsync(IProcess process)
        {
            await _processCommands.SaveProcessStateAsync(process);
        }
    }
}
