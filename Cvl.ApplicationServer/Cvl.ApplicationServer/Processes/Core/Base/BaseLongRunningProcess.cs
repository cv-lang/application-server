using Cvl.ApplicationServer.Processes.LongRunningProcesses;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Processes.Core.Base
{
    public enum ProcessHibernationType
    {
        DelayOfProcessExecution,
        WaitingForExternalData,
        WaitingForUserInterface,
        Error,
        Executed
    }

    public abstract class BaseLongRunningProcess : ILongRunningProcess
    {
        public ProcessData? ProcessData { get; set; }

        [Interpret]
        public abstract LongRunningProcessResult StartProcess(object inputParam);   
        
    }
}
