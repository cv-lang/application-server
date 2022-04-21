using Cvl.ApplicationServer.Processes.Base;

namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public class LongRunningProcessData : ProcessData
    {
        internal VirtualMachine.VirtualMachine VirtualMachine { get; set; }
    }
}
