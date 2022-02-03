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
using Cvl.ApplicationServer.Processes.UI;
using Cvl.VirtualMachine.Core;
using Cvl.VirtualMachine.Core.Variables.Values;

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
            return (BaseProcess)_processQueries.LoadProcessAsync<BaseProcess>(processNumber).Result;
        }

        public void SaveProcess(IProcess process)
        {
            _processCommands.SaveProcessStateAsync(process).Wait();
        }

        public ProcessStatus StartLongRunningProcess<T>(object inputParameter) where T : ILongRunningProcess
        {
            var processStatus = new ProcessStatus();

            var process = CreateProcess<T>();

            var vm = new VirtualMachine.VirtualMachine();
            process.LongRunningProcessData.VirtualMachine = vm;

            var result = vm.Start<object>("Start", process);
            if (vm.Thread.Status == VirtualMachineState.Hibernated)
            {
                
                process.ProcessData.ThreadStatus = Processes.Threading.ThreadState.WaitingForExecution;

                SaveProcess(process);

                processStatus.Status = ProcessExecutionStaus.Pending;
                processStatus.ProcessId = process.ProcessData.ProcessId;
                processStatus.ProcessNumber = process.ProcessData.ProcessNumber;
                

                return processStatus;
            }
            else
            {
                throw new Exception("Process end immediately. It should use hibernation for long running process");
            }
        }

        public void RunProcesses()
        {
            var processesNumbers = _processQueries.GetWaitingForExecutionProcessesNumbersAsync().Result;

            foreach (var processesNumber in processesNumbers)
            {
                var process = LoadProcess(processesNumber) as ILongRunningProcess;
                process.Resume();
            }
        }
    }
}
