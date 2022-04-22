using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.LongRunningProcesses;
using Cvl.ApplicationServer.Processes.Interfaces;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses
{
    /// <summary>
    /// Public service API for running processes and communcaton with process and external world
    /// </summary>
    public interface ILongRunningProcessesService
    {
        /// <summary>
        /// Create new process, save it to db and run
        /// Running proces can exit imidetly and return computed value or by hibernate/suspend and waiting for execution 
        /// or waiting for external data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputParameter"></param>
        /// <returns></returns>
        Task<LongRunningProcessResult> StartLongRunningProcessAsync<T>(object inputParameter) where T : ILongRunningProcess;

        /// <summary>
        /// Get a information about process state
        /// Process can be in pending process - that mean that process is executing now or is pending for execution
        /// if process is Delay'ed is also pending (eg. if proces is Delay(2 days) it'll be in pending state for 2 days)
        /// </summary>
        /// <param name="processNumber"></param>
        /// <returns></returns>
        Task<LongRunningProcessResult> GetProcessStatusAsync(string processNumber);

        /// <summary>
        /// Get external data. If process is in WaitingForExternalData or WaitingForUserInterface state, this method return data from process
        /// (for external users/uses)
        /// </summary>
        /// <param name="processNumber"></param>
        /// <returns></returns>
        Task<object?> GetProcessExternalDataAsync(string processNumber);

        /// <summary>
        /// Set external data to process (send data from outsite)
        /// </summary>
        /// <param name="processNumber"></param>
        /// <param name="externalData"></param>
        /// <returns></returns>
        Task SetProcessExternalDataAsync(string processNumber, object? externalData);        
    }
}
