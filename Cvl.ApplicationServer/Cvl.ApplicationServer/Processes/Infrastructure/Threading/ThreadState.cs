namespace Cvl.ApplicationServer.Core.Processes.Threading
{
    public enum ThreadState
    {
        Idle = 0,
        WaitingForExecution,
        WaitingForExternalData,
        WaitingForUserInterface,
        Error,
        Executed
    }
}
