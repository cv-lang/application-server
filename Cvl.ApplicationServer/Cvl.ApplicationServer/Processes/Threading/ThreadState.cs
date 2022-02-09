using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Threading
{
    public enum ThreadState
    {
        Idle = 0,
        WaitingForExecution,
        WaitForExternalData,
        WaitingForUserInterface,
        Error,
        Executed
    }
}
