using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Processes.Commands;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Model.OwnedClasses;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.SimpleProcesses;
using Cvl.ApplicationServer.Processes.StepBaseProcesses.Worker;

namespace Cvl.ApplicationServer.Processes.StepBaseProcesses.Workers
{
    internal class StepBaseProcessWorker : IStepBaseProcessWorker
    {
        private readonly ProcessCommands _processCommands;
        private readonly ProcessQueries _processQueries;
        private readonly IFullSerializer _fullSerializer;

        public StepBaseProcessWorker(ProcessCommands processCommands, ProcessQueries processQueries,
            IFullSerializer fullSerializer)
        {
            _processCommands = processCommands;
            _processQueries = processQueries;
            _fullSerializer = fullSerializer;
        }

        public async Task<TimeSpan> RunProcessesAsync()
        {
            var processesNumbers = await _processQueries.GetWaitingForExecutionProcessesNumbersAsync(ProcessType.StepBaseProcess);

            foreach (var processNumber in processesNumbers)
            {
                var process = await _processQueries.LoadProcessAsync<IStepBaseProcess>(processNumber);
                process.JobEntry();

                //zapisuje stan procesu
                await _processCommands.SaveProcessStateAsync(process);
            }

            return TimeSpan.FromSeconds(30);
        }

        public async Task RunLoopProcessesAsync()
        {
            while (true)
            {
                var delay = await RunProcessesAsync();
                await Task.Delay(delay);
            }
        }
    }
}
