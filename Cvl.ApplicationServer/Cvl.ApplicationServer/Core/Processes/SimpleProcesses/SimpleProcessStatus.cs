using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Processes.SimpleProcesses
{
    public class SimpleProcessStatus
    {
        public SimpleProcessStatus(string processNumber)
        {
            ProcessNumber = processNumber;
        }

        public string ProcessNumber { get; set; }
    }
}
