using Cvl.ApplicationServer.Core.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses
{
    /// <summary>
    /// Worker responsible for execution processes
    /// </summary>
    public interface ILongRunningProcessWorker
    {
        Task<TimeSpan> RunProcessesAsync();
        Task RunLoopProcessesAsync();
        Task<LongRunningProcessResult> StartLongRunningProcessAsync<TProcess>(object inputParameter)
            where TProcess : ILongRunningProcess;
    }
}
