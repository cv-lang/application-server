using Cvl.ApplicationServer.Processes.Core.Base;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses
{
    /// <summary>
    /// main interface for long running process
    /// </summary>
    public interface ILongRunningProcess : IProcess
    { 
        LongRunningProcessResult StartProcess(object inputParam);
    }

    public class LongRunningProcessResult
    {
        public LongRunningProcessState State { get; set; } = LongRunningProcessState.Executed;
        public object? Result { get; set; }
        public long? ProcessId { get; set; }
        public string? ProcessNumber { get; set; }
    }    

    public enum LongRunningProcessState
    {
        //waiting for execution or is executing
        Pending,
        //waiting for external data or event
        WaitingForExternalData,
        //waiting for user interface data or event
        WaitingForUserInterface,
        Executed,
        Error        
    }

    
}
