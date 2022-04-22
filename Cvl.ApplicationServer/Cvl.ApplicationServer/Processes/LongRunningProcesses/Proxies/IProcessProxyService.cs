using Cvl.ApplicationServer.Core.Processes.LongRunningProcesses;
using Cvl.ApplicationServer.Processes.Interfaces;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Services
{
    public interface IProcessProxyService
    {
        /// <summary>
        /// Otwarcie procesu - raczej nie powinno się używać, w normalny przypadku powinie proces przchodzić przez eventy zewnętrzne
        /// to można do zmiany stanu, poza procesem (coś a'la wykonanie w innym wątku)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processNumber"></param>
        /// <returns></returns>
        Task<LongRunningProcessProxy<T>> OpenProcessProxyAsync<T>(string processNumber) where T : IProcess;
    }

    public class LongRunningProcessProxy<T> : IDisposable
        where T : IProcess
    {
        private readonly LongRunningProcessesService _applicationServerProcesses;

        internal LongRunningProcessProxy(T process, LongRunningProcessesService applicationServerProcesses)
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
}
