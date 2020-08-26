namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class ProcessDescription
    {
        public EnumProcessStatus ProcessStatus { get; set; }
        public string ProcessTypeFullName { get; internal set; }

        public FormData FormData { get; internal set; }
    }
}
