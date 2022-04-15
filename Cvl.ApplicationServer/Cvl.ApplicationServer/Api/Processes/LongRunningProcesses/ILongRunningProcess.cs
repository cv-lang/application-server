using Cvl.VirtualMachine;

namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public class LongRunningProcessResult
    {
        public LongRunningProcessState State { get; set; } = LongRunningProcessState.Executed;
        public object? Result { get; set; }
        public long? ProcessId { get; set; }
        public string? ProcessNumber { get; set; }
    }    

    public enum LongRunningProcessState
    {
        //czeka na wykonanie lub jest wykonywany
        Pending,
        //czeka na dane lub event z zewnątrz
        WaitingForExternalData,
        //czekam na dane z interfejsu
        WaitingForUserInterface,
        Executed,
        Error        
    }

    /// <summary>
    /// Long running process
    /// </summary>
    public interface ILongRunningProcess : IProcess
    {
        LongRunningProcessData LongRunningProcessData { get; set; }
        LongRunningProcessResult StartLongRunningProcess(object inputParam);
    }
}
