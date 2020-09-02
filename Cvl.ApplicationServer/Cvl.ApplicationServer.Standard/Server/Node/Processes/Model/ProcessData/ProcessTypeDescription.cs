namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class ProcessTypeDescription
    {
        public string ProcessTypeFullName { get; set; }
        public string AssemblyQualifiedName { get; internal set; }
        public string AssemblyDirectoryPath { get; internal set; }
    }
}
