using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.ApplicationServer.Processes.Interfaces;

namespace Cvl.ApplicationServer.Processes
{
    public interface IProcessManager
    {
        IProcess Process { get; set; }

        void SetStep(string stepName, string description, object step);
        void SetExternalData(string processNumber, object externalData = null);
        object GetExternalData(string processNumber);

        void SetToJobThread();
        void SetToApiThread();
    }
}
