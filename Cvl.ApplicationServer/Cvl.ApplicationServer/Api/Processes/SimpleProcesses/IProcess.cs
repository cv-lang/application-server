using Cvl.ApplicationServer.Processes;

namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public interface IStepBaseProcess : IProcess
    {
        void JobEntry();
    }

    public interface IProcess
    {
        ProcessData ProcessData { get; set; }
        object? GetProcessState();
        void LoadProcessState(object? serializedState);
    }
}
