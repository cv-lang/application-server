using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.StepProcesses
{
    public interface IProcessManagerFactory
    {
        IProcessManager CreateProcessManager(IProcess process);
    }
}
