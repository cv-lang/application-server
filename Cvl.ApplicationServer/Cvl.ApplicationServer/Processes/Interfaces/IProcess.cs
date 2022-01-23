using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Old.Processes.Infrastructure;

namespace Cvl.ApplicationServer.Processes.Interfaces2
{
    public interface IProcess
    {
        ProcessData ProcessData { get; set; }
        object GetProcessState();
        void LoadProcessState(object serializedState);
    }
}
