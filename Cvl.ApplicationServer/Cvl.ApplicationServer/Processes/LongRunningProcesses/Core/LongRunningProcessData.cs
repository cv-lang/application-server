using Cvl.ApplicationServer.Processes.Core.Base;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Core
{
    public class LongRunningProcessData : ProcessData
    {
        internal VirtualMachine.VirtualMachine VirtualMachine { get; set; }
    }
}
