using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model.Processes;

namespace Cvl.ApplicationServer.Old.Processes.Infrastructure
{
    public class ProcessData
    {
        internal ProcessInstanceContainer ProcessInstanceContainer { get; set; }
        public long ProcessId => ProcessInstanceContainer.Id;
        public string ProcessNumber => ProcessInstanceContainer.ProcessNumber;
    }
}
