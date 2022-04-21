using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.LongRunningProcesses;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses
{
    public interface ILongRunningProcessesService
    {
        /// <summary>
        /// Uruchomienie procesu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputParameter"></param>
        /// <returns></returns>
        Task<LongRunningProcessResult> StartLongRunningProcessAsync<T>(object inputParameter) where T : ILongRunningProcess;

        Task<LongRunningProcessResult> GetProcessStatusAsync(string processNumber);

        Task<object?> GetProcessExternalDataAsync(string processNumber);
        Task SetProcessExternalDataAsync(string processNumber, object? externalData);

        /// <summary>
        /// Otwarcie procesu - raczej nie powinno się używać, w normalny przypadku powinie proces przchodzić przez eventy zewnętrzne
        /// to można do zmiany stanu, poza procesem (coś a'la wykonanie w innym wątku)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="processNumber"></param>
        /// <returns></returns>
        Task<LongRunningProcessProxy<T>> OpenProcessProxyAsync<T>(string processNumber) where T : IProcess;
    }
}
