using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.LongRunningProcesses;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses
{
    public interface IApplicationServerLongRunningProcesses
    {
        Task<LongRunningProcessStatus> StartLongRunningProcessAsync<T>(object inputParameter) where T : ILongRunningProcess;
        Task<LongRunningProcessProxy<T>> OpenProcessProxyAsync<T>(string processNumber) where T : IProcess;
    }
}
