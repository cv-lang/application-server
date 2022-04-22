namespace Cvl.ApplicationServer.Processes.Core.Base
{
    public interface IProcess
    {        
        ProcessData? ProcessData { get; set; }       
    }

    public interface IStateProcess
    {
        object? GetProcessState();
        void LoadProcessState(object? serializedState);
    }
}
