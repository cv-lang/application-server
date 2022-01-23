using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Old.Processes.Infrastructure;
using Cvl.ApplicationServer.Processes.Interfaces2;

namespace Cvl.ApplicationServer.Processes
{
    public abstract class BaseProcess : IProcess
    {
        public ProcessData ProcessData { get; set; }

        public abstract void LoadProcessState(object processState);

        public abstract object GetProcessState();
    }
}
