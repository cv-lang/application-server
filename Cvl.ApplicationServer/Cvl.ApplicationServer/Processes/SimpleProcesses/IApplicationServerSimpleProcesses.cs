using System.Diagnostics;
using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.SimpleProcesses;
using Cvl.ApplicationServer.Core.Processes.UI;

namespace Cvl.ApplicationServer.Processes
{
    public interface IApplicationServerSimpleProcesses
    {
        Task<SimpleProcessStatus> StartProcessAsync<T>() where T : IProcess;
        Task<SimpleProcessProxy<T>> OpenProcessProxyAsync<T>(string processNumber) where T : IProcess;
    }
}
