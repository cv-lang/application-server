using Cvl.ApplicationServer.Processes.Core.Base;

namespace Cvl.ApplicationServer.Processes.StepBaseProcesses
{
    public interface IApplicationServerSimpleProcesses
    {
        Task<SimpleProcessStatus> StartProcessAsync<T>() where T : IProcess;
        Task<SimpleProcessProxy<T>> OpenProcessProxyAsync<T>(string processNumber) where T : IProcess;
    }
}
