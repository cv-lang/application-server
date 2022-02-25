namespace Cvl.ApplicationServer.Core.Processes.Threading
{
    public enum ThreadState
    {
        Idle = 0,
        WaitingForExecution,
        WaitForExternalData,
        WaitingForUserInterface,
        Error,
        Executed
    }
}
