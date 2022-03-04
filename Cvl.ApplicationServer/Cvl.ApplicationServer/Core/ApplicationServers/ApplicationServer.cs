using Cvl.ApplicationServer.Processes;

namespace Cvl.ApplicationServer.Core.ApplicationServers
{
    public class ApplicationServer : IApplicationServer
    {
        public ApplicationServer(IApplicationServerSimpleProcesses simpleProcesses)
        {
            SimpleProcesses = simpleProcesses;
        }

        public IApplicationServerSimpleProcesses SimpleProcesses { get; set; }
    }
}
