using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Dtos;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.UI;

namespace Cvl.ApplicationServer.Processes
{
    public interface IApplicationServerProcesses
    {
        T CreateProcess<T>() where T : IProcess;
        void SaveProcess(IProcess process);
        IProcess LoadProcess(string processNumber);
        T LoadProcess<T>(string processNumber) where T : IProcess;
        T OpenProcessProxy<T>(string processNumber) where T : IProcess, IDisposable;
        ProcessStatus StartLongRunningProcess<T>(object inputParameter) where T : ILongRunningProcess;
    }
}
