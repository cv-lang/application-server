using Cvl.ApplicationServer.Processes.Core.UI;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Services
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
        Task<LongRunningProcessResult> StartLongRunningProcessAsync<T>(object? inputParameter) where T : ILongRunningProcess;

        /// <summary>
        /// Get a information about process state
        /// Process can be in pending process - that mean that process is executing now or is pending for execution
        /// if process is Delay'ed is also pending (eg. if proces is Delay(2 days) it'll be in pending state for 2 days)
        /// </summary>
        /// <param name="processNumber"></param>
        /// <returns></returns>
        Task<LongRunningProcessResult> GetProcessStatusAsync(string processNumber);

        /// <summary>
        /// Get external data. If process is in WaitingForExternalData state, this method return data from process
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

        /// <summary>
        /// Get current process view. If process is in WaitingForUserInterface stte, return view data
        /// </summary>
        /// <param name="processNumber"></param>
        /// <returns></returns>
        Task<View?> GetProcessViewAsync(string processNumber);
        /// <summary>
        /// Set view response to process (send data from outsite)
        /// </summary>
        /// <returns></returns>
        Task SetProcessViewDataAsync(string processNumber, object? viewResponse);
    }
}
