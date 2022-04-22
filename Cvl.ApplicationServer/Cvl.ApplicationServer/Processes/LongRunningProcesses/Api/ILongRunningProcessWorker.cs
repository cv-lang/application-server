using Cvl.ApplicationServer.Core.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses
{
    public interface ILongRunningProcessWorker
    {
        Task<int> RunProcessesAsync();
        Task<LongRunningProcessResult> StartLongRunningProcessAsync<TProcess>(object inputParameter)
            where TProcess : ILongRunningProcess;
    }
}
