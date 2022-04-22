namespace Cvl.ApplicationServer.Processes.Core.Threading
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
