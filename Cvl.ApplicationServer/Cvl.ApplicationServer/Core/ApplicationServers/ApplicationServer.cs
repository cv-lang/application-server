using Cvl.ApplicationServer.Processes;

namespace Cvl.ApplicationServer.Core.ApplicationServers
{
    public class ApplicationServer : IApplicationServer
    {
        public ApplicationServer(IApplicationServerProcesses processes)
        {
            Processes = processes;
        }

        public IApplicationServerProcesses Processes { get; set; }
    }
}
