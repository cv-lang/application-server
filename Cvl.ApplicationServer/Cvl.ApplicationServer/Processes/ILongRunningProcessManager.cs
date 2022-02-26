using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.UI;

namespace Cvl.ApplicationServer.Processes
{
    public interface ILongRunningProcessManager : IProcessManager
    {
        void Delay(DateTime delayUntil);
        object WaitForExternalData(object data = null);
        ViewResponse ShowView(View view);
    }
}
