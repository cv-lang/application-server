using Cvl.ApplicationServer.Processes;

namespace Cvl.ApplicationServer
{
    public interface IApplicationServer
    {
        IApplicationServerProcesses Processes { get; }
    }
}
