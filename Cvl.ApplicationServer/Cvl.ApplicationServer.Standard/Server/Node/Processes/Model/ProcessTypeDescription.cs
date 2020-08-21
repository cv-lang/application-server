namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class ProcessTypeDescription
    {
        public string ProcessTypeFullName { get; set; }
        public string AssemblyFullName { get; internal set; }
        public string AssemblyDirectoryPath { get; internal set; }
    }
}
