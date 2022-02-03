using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.ApplicationServers.Internals
{
    public enum ProcessExecutionStaus
    {
        Succes,
        Pending,
        Error
    }
    public class ProcessStatus
    {
        public ProcessExecutionStaus Status { get; internal set; }
        public long ProcessId { get; set; }
        public string ProcessNumber { get; internal set; }
    }
}
