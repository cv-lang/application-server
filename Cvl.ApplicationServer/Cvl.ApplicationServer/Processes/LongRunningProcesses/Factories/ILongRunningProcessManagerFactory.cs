using Cvl.ApplicationServer.Core.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses
{
    public interface ILongRunningProcessManagerFactory
    {
        ILongRunningProcessManager CreateProcessManager(ILongRunningProcess process);
    }
}
