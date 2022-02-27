using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Processes
{
    public interface ILongRunningProcessManager : IProcessManager
    {
        [Interpret]
        void Delay(DateTime delayUntil);
        [Interpret]
        object WaitForExternalData(object data = null);
        [Interpret]
        ViewResponse ShowView(View view);
    }
}
