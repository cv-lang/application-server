using Cvl.ApplicationServer.Processes.LongRunningProcesses.Managers;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Factories
{
    public interface ILongRunningProcessManagerFactory
    {
        ILongRunningProcessManager CreateProcessManager(ILongRunningProcess process);
    }
}
