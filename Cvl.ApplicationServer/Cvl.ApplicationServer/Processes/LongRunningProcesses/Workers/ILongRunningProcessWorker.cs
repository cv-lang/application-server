namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Workers
{
    /// <summary>
    /// Worker responsible for execution processes
    /// </summary>
    public interface ILongRunningProcessWorker
    {
        Task<TimeSpan> RunProcessesAsync();
        Task RunLoopProcessesAsync();
        Task<LongRunningProcessResult> StartLongRunningProcessAsync<TProcess>(object inputParameter)
            where TProcess : ILongRunningProcess;
    }
}
