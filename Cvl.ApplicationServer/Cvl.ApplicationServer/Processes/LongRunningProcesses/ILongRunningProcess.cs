using Cvl.VirtualMachine;

namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public interface ILongRunningProcess : IProcess
    {
        VirtualMachineResult<object> StartLongRunningProcess(object inputParam);
        VirtualMachineResult<object> ResumeLongRunningProcess(object? inputData);
    }
}
