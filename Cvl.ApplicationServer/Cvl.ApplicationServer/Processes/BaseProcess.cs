using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes.Interfaces2;

namespace Cvl.ApplicationServer.Processes
{
    public class BaseProcess : IProcess
    {
        public long ProcessId { get; set; }
        
    }
}
