using System.Xml.Serialization;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.ApplicationServer.Processes.Base;
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

    public abstract class BaseLongRunningProcess : ILongRunningProcess
    {
        public ProcessData? ProcessData { get; set; }

        [Interpret]
        public abstract LongRunningProcessResult StartProcess(object inputParam);   
        
    }
}
