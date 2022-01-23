using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Interfaces;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Commands;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces2;
using Cvl.ApplicationServer.Processes.Queries;
using Cvl.VirtualMachine.Core;

namespace Cvl.ApplicationServer.ApplicationServers.Internals
{
    public class ApplicationServerProcesses : Cvl.ApplicationServer.Core.Interfaces.IApplicationServerProcesses
    {
        private readonly ProcessCommands _processCommands;
        private readonly ProcessQueries _processQueries;
        private readonly IFullSerializer _fullSerializer;

        public ApplicationServerProcesses(ProcessCommands processCommands, ProcessQueries processQueries, 
            IFullSerializer fullSerializer)
        {
            _processCommands = processCommands;
            _processQueries = processQueries;
            _fullSerializer = fullSerializer;
        }
        public T CreateProcess<T>() where T : IProcess
        {
            return _processCommands.CreateProcessAsync<T>().Result;
        }

        public IProcess LoadProcess(string processNumber)
        {
            return (BaseProcess) _processQueries.LoadProcessAsync<BaseProcess>(processNumber).Result;
        }

        public void SaveProcess(IProcess process)
        {
            _processCommands.SaveProcessStateAsync(process).Wait();
        }

        public object StartProcess<T>(object inputParameter) where T : IProcess
        {
            var process = CreateProcess<T>();

            var vw = new VirtualMachine.VirtualMachine();
            vw.Start<object>("Start", process, inputParameter);
            if (vw.Thread.Status == VirtualMachineState.Hibernated)
            {
                //vw.Serializer();
            }
            else
            {
                
            }

            return process;
        }
    }
}
