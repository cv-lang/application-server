using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Core.Base;
using Cvl.ApplicationServer.Processes.Core.Queries;
using Cvl.ApplicationServer.Processes.Core.UI;
using Cvl.ApplicationServer.Processes.StepBaseProcesses.Managers;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Managers
{
    internal class LongRunningProcessManager: ProcessManager, ILongRunningProcessManager
    {
        public LongRunningProcessManager(ProcessQueries processQueries, IFullSerializer fullSerializer) :
            base(processQueries, fullSerializer)
        {
        }

        [Interpret]
        public void Delay(DateTime delayUntil)
        {
            VirtualMachine.VirtualMachine.Hibernate(ProcessHibernationType.DelayOfProcessExecution,
                delayUntil);
        }

        [Interpret]
        public object? WaitForExternalData(object? data = null)
        {
            var result = VirtualMachine.VirtualMachine.Hibernate(ProcessHibernationType.WaitingForExternalData, data);

            return result;
        }

        [Interpret]
        public object ShowView(View view)
        {
            var response = VirtualMachine.VirtualMachine.Hibernate(
                ProcessHibernationType.WaitingForUserInterface, view)!;
            return response;
        }

        [Interpret]
        public void EndProcess(string status, object? resultData = null,  View? endView = null)
        {
            VirtualMachine.VirtualMachine
                .Hibernate(ProcessHibernationType.Executed, status, resultData, endView);
        }
    }
}
