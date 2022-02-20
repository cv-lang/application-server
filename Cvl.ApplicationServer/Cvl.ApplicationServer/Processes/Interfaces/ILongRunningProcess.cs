using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes.Interfaces2;
using Cvl.VirtualMachine;

namespace Cvl.ApplicationServer.Processes.Interfaces
{
    public interface ILongRunningProcess : IProcess
    {
        LongRunningProcessData LongRunningProcessData { get; set; }
        object Start(object inputParam);

        VirtualMachineResult<object> ResumeLongRunningProcess(object? inputData);
    }
}
