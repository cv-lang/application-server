using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.UI;

namespace Cvl.ApplicationServer.Processes
{


    public abstract class BaseLongRunningProcess : BaseProcess, ILongRunningProcess
    {
        public LongRunningProcessData LongRunningProcessData { get; set; } = new LongRunningProcessData();

        public abstract object Start(object inputParam);

        public virtual void Resume()
        {
            LongRunningProcessData.VirtualMachine.Resume<object>();
        }

        #region Process state serialization/deserializaton

        public override object GetProcessState()
        {
            return LongRunningProcessData.VirtualMachine;
        }

        public override void LoadProcessState(object processState)
        {
            LongRunningProcessData.VirtualMachine = (VirtualMachine.VirtualMachine)processState;
        }

        #endregion

        #region hibernation and UI

        protected ViewResponse ShowView(View view)
        {
            var response = (ViewResponse)VirtualMachine.VirtualMachine.Hibernate(
                Cvl.ApplicationServer.Processes.Threading.ThreadState.WaitingForUserInterface, view);
            return response;
        }

        #endregion
    }
}
