using Cvl.ApplicationServer.Processes;

namespace Cvl.ApplicationServer
{
    public interface IApplicationServer
    {
        IApplicationServerSimpleProcesses SimpleProcesses { get; }
    }
}
