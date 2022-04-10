using System.Xml.Serialization;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.VirtualMachine;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Core.Processes
{
    public enum ProcessHibernationType
    {
        DelayOfProcessExecution,
        WaitForExternalData,
        WaitingForUserInterface
    }

    public abstract class BaseLongRunningProcess : BaseProcess, ILongRunningProcess
    {

        [Interpret]
        public abstract VirtualMachineResult<object> StartLongRunningProcess(object inputParam);

        [Interpret]
        public virtual VirtualMachineResult<object> ResumeLongRunningProcess(object inputData)
        {
            var vmResult = ProcessData.LongRunningProcessData.VirtualMachine.Resume<object>(inputData);
            return vmResult;
        }

        #region Process state serialization/deserializaton

        public override object GetProcessState()
        {
            return ProcessData?.LongRunningProcessData?.VirtualMachine;
        }

        public override void LoadProcessState(object processState)
        {
            ProcessData.LongRunningProcessData.VirtualMachine = (VirtualMachine.VirtualMachine)processState;
            ProcessData.LongRunningProcessData.VirtualMachine.Instance = this;
        }

        #endregion

        
    }
}
