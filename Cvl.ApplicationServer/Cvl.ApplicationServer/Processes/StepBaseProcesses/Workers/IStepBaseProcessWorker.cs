using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.StepBaseProcesses.Worker
{
    public interface IStepBaseProcessWorker
    {
        Task<TimeSpan> RunProcessesAsync();
        Task RunLoopProcessesAsync();
    }
}
