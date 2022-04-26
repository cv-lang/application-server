using Cvl.ApplicationServer.Processes.Core.UI;
using Cvl.ApplicationServer.Processes.StepBaseProcesses.Managers;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Managers
{
    /// <summary>
    /// In process, process state,step, external comunication.. manager
    /// </summary>
    public interface ILongRunningProcessManager : IProcessManager
    {
        [Interpret]
        void Delay(DateTime delayUntil);
        [Interpret]
        object? WaitForExternalData(object? data = null);
        [Interpret]
        object ShowView(View view);

        [Interpret]
        void EndProcess(string status, object? resultData = null, View? endView = null);
    }
}
