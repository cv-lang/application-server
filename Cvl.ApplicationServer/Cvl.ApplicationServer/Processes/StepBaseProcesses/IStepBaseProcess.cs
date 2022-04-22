using Cvl.ApplicationServer.Processes.Core.Base;

namespace Cvl.ApplicationServer.Processes.StepBaseProcesses
{
    public interface IStepBaseProcess : IProcess, IStateProcess
    {
        void JobEntry();
    }
}
