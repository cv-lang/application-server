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
        [Interpret]
        public abstract LongRunningProcessResult StartProcess(object inputParam);

        #region Process state serialization/deserializaton

        public override object? GetProcessState()
        {
            return ((LongRunningProcessData?)ProcessData)?.VirtualMachine;
        }

        public override void LoadProcessState(object? processState)
        {
            var processData = ((LongRunningProcessData?)ProcessData);
            processData.VirtualMachine = (VirtualMachine.VirtualMachine)processState;
            processData.VirtualMachine.Instance = this;
        }

        #endregion

        
    }
}
