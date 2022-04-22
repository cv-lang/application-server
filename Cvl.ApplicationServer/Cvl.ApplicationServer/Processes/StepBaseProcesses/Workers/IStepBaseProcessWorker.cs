namespace Cvl.ApplicationServer.Processes.StepBaseProcesses.Workers
{
    public interface IStepBaseProcessWorker
    {
        Task<TimeSpan> RunProcessesAsync();
        Task RunLoopProcessesAsync();
    }
}
