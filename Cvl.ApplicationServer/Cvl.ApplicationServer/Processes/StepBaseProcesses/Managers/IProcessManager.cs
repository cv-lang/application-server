using Cvl.ApplicationServer.Processes.Core.Base;

namespace Cvl.ApplicationServer.Processes.StepBaseProcesses.Managers
{
    public interface IProcessManager
    {
        IProcess Process { get; set; }

        void SetStep(string stepName, string description, object step);
        void SetExternalData(string processNumber, object externalData = null);
        object GetExternalData(string processNumber);

        void SetToJobThread();
        void SetToApiThread();
    }
}
