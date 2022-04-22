using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.SimpleProcesses
{
    public interface IStepBaseProcess : IProcess, IStateProcess
    {
        void JobEntry();
    }
}
