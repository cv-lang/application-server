using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Interfaces;

namespace Cvl.ApplicationServer.ApplicationServers
{
    public class ApplicationServer : IApplicationServer
    {
        public ApplicationServer(IApplicationServerProcesses processes)
        {
            Processes = processes;
        }

        public IApplicationServerProcesses Processes { get; set; }
    }
}
