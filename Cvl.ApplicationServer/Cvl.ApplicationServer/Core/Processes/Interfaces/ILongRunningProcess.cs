using Cvl.VirtualMachine;

namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public interface ILongRunningProcess : IProcess
    {
        LongRunningProcessData LongRunningProcessData { get; set; }
        object Start(object inputParam);

        VirtualMachineResult<object> ResumeLongRunningProcess(object? inputData);
    }
}
