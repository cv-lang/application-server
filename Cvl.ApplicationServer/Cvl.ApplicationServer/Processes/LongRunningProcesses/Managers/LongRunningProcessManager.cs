using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes;
using Cvl.VirtualMachine.Core.Attributes;

namespace Cvl.ApplicationServer.Core.Processes.Services
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
        public ViewResponse ShowView(View view)
        {
            var response = (ViewResponse)VirtualMachine.VirtualMachine.Hibernate(
                ProcessHibernationType.WaitingForUserInterface, view)!;
            return response;
        }
    }
}
