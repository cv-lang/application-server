using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Interfaces
{
    public interface IApplicationServer
    {
        Cvl.ApplicationServer.Core.Interfaces.IApplicationServerProcesses Processes { get; }
    }
}
