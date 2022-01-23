using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes.Interfaces;

namespace Cvl.ApplicationServer.Processes
{


    public abstract class BaseLongRunningProcess : BaseProcess, IStartable
    {
        public abstract object Start(object inputParam);

        internal VirtualMachine.VirtualMachine virtualMachine { get; set; }

        public override object GetProcessState()
        {
            return virtualMachine;
        }

        public override void LoadProcessState(object processState)
        {
            virtualMachine = (VirtualMachine.VirtualMachine)processState;
        }
    }
}
