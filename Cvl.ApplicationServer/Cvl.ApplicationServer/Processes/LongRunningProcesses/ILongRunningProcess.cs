using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.VirtualMachine;

namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    /// <summary>
    /// Long running process
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
        //czeka na wykonanie lub jest wykonywany
        Pending,
        //czeka na dane lub event z zewnątrz
        WaitingForExternalData,
        //czekam na dane z interfejsu
        WaitingForUserInterface,
        Executed,
        Error        
    }

    
}
