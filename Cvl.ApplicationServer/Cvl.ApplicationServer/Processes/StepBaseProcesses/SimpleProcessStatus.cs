namespace Cvl.ApplicationServer.Processes.StepBaseProcesses
{
    public class SimpleProcessStatus
    {
        public SimpleProcessStatus(string processNumber)
        {
            ProcessNumber = processNumber;
        }

        public string ProcessNumber { get; set; }
    }
}
