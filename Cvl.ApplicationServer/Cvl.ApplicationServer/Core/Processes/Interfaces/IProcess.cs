namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public interface IProcess
    {
        ProcessData ProcessData { get; set; }
        object GetProcessState();
        void LoadProcessState(object? serializedState);

        void JobEntry();
    }
}
