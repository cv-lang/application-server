using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces2;
using Cvl.ApplicationServer.Test;

namespace Cvl.ApplicationServer.Core.Interfaces
{
    public interface IApplicationServerProcesses
    {
        T CreateProcess<T>() where T : IProcess;
        void SaveProcess(IProcess process);
        IProcess LoadProcess(string processNumber);
        object StartProcess<T>(object inputParameter) where T : IProcess;
    }
}
