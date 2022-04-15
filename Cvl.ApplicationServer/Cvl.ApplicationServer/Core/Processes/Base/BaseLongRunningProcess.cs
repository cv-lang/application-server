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
        WaitingForExternalData,
        WaitingForUserInterface
    }

    public abstract class BaseLongRunningProcess : BaseProcess, ILongRunningProcess
    {
        public LongRunningProcessData LongRunningProcessData { get; set; } = new LongRunningProcessData();

        [Interpret]
        public abstract LongRunningProcessResult StartLongRunningProcess(object inputParam);

        #region Process state serialization/deserializaton

        public override object GetProcessState()
        {
            return LongRunningProcessData?.VirtualMachine;
        }

        public override void LoadProcessState(object processState)
        {
            LongRunningProcessData.VirtualMachine = (VirtualMachine.VirtualMachine)processState;
            LongRunningProcessData.VirtualMachine.Instance = this;
        }

        #endregion

        
    }
}
