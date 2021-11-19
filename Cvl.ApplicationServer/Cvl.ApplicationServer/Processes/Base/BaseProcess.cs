using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Base
{
    [ThreadType(ThreadType = ThreadType.Multithreaded)]
    public class BaseProcess : IProcess
    {
    }
}
