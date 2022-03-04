﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Interfaces;

namespace Cvl.ApplicationServer.Processes.Workers
{
    public interface IProcessesWorker : IWorker
    {
        Task<int> RunProcessesAsync();
        Task<LongRunningProcessStatus> StartLongRunningProcessAsync<T>(object inputParameter) where T : ILongRunningProcess;
    }
}
