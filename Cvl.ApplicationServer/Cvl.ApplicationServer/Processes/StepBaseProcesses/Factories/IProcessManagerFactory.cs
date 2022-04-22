using Cvl.ApplicationServer.Processes.Core.Base;
using Cvl.ApplicationServer.Processes.StepBaseProcesses.Managers;

namespace Cvl.ApplicationServer.Processes.StepBaseProcesses.Factories
{
    public interface IProcessManagerFactory
    {
        IProcessManager CreateProcessManager(IProcess process);
    }
}
