using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes.Interfaces2;

namespace Cvl.ApplicationServer.Processes.Interfaces
{
    public interface ILongRunningProcess : IProcess
    {
        object Start(object inputParam);
    }
}
