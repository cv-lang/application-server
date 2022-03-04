namespace Cvl.ApplicationServer.Core.ApplicationServers.Internals
{
    public enum ProcessExecutionStaus
    {
        Succes,
        Pending,
        Error
    }
    public class LongRunningProcessStatus
    {
        public LongRunningProcessStatus(ProcessExecutionStaus status, long processId, string processNumber)
        {
            Status = status;
            ProcessId = processId;
            ProcessNumber = processNumber;
        }

        public ProcessExecutionStaus Status { get; set; }
        public long ProcessId { get; set; }
        public string ProcessNumber { get; set; }
    }
}
