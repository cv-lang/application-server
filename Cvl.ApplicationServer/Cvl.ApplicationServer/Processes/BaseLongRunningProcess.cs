using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.UI;
using Cvl.VirtualMachine;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Processes
{
    public enum ProcessHibernationType
    {
        DelayOfProcessExecution,
        WaitForExternalData,
        WaitingForUserInterface
    }

    public abstract class BaseLongRunningProcess : BaseProcess, ILongRunningProcess
    {
        public LongRunningProcessData LongRunningProcessData { get; set; } = new LongRunningProcessData();
        

        [Interpret]
        public abstract object Start(object inputParam);

        public virtual VirtualMachineResult<object> Resume(object inputData)
        {
            var vmResult = LongRunningProcessData.VirtualMachine.Resume<object>(inputData);
            return vmResult;
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

        [Interpret]
        public void Delay(DateTime delayUntil)
        {
            VirtualMachine.VirtualMachine.Hibernate(ProcessHibernationType.DelayOfProcessExecution, 
                delayUntil);
        }

        [Interpret]
        public object WaitForExternalData(object data = null)
        {
            var result = VirtualMachine.VirtualMachine.Hibernate(ProcessHibernationType.WaitForExternalData, data);

            return result;
        }

        [Interpret]
        public ViewResponse ShowView(View view)
        {
            var response = (ViewResponse)VirtualMachine.VirtualMachine.Hibernate(
                ProcessHibernationType.WaitingForUserInterface, view);
            return response;
        }

        #endregion
    }
}
