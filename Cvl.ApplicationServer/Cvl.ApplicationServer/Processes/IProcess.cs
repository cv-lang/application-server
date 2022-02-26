using Cvl.ApplicationServer.Processes;

namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public interface IProcess : IJob
    {
        ProcessData ProcessData { get; set; }
        object GetProcessState();
        void LoadProcessState(object? serializedState);

    }
}
