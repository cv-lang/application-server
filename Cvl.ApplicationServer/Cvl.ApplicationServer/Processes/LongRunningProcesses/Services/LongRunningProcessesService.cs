﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.ApplicationServers.Internals;
using Cvl.ApplicationServer.Core.Processes.Commands;
using Cvl.ApplicationServer.Core.Processes.Interfaces;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Cvl.ApplicationServer.Core.Processes.UI;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.LongRunningProcesses;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Processes.LongRunningProcesses
{   

    internal class LongRunningProcessesService : ILongRunningProcessesService
    {
        private readonly ProcessQueries _processQueries;
        private readonly ProcessInstanceContainerQueries _processInstanceContainerQueries;
        private readonly ILongRunningProcessWorker _worker;
        private readonly ProcessCommands _processCommands;
        private readonly ProcessInstanceContainerCommands _processInstanceContainerCommands;
        private readonly IFullSerializer _fullSerializer;

        public LongRunningProcessesService(ProcessQueries processQueries, ProcessInstanceContainerQueries processInstanceContainerQueries,
            ILongRunningProcessWorker worker, ProcessCommands processCommands, 
            ProcessInstanceContainerCommands processInstanceContainerCommands,
            IFullSerializer fullSerializer)
        {
            _processQueries = processQueries;
            _processInstanceContainerQueries = processInstanceContainerQueries;
            _worker = worker;
            _processCommands = processCommands;
            _processInstanceContainerCommands = processInstanceContainerCommands;
            _fullSerializer = fullSerializer;
        }        

        public async Task<LongRunningProcessResult> StartLongRunningProcessAsync<TProcess>(object inputParameter) 
            where TProcess : ILongRunningProcess
        {
            return await _worker.StartLongRunningProcessAsync<TProcess>(inputParameter);
        }

        public async Task<LongRunningProcessResult> GetProcessStatusAsync(string processNumber)
        {
            var processInstanceContainer = await _processInstanceContainerQueries.GetAll()
                .SingleAsync(x => x.ProcessNumber == processNumber);

            var result = new LongRunningProcessResult()
            {
                ProcessId = processInstanceContainer.Id,
                ProcessNumber = processNumber,
            };

            switch(processInstanceContainer.ThreadData.MainThreadState)
            {
                case Threading.ThreadState.Idle:
                case Threading.ThreadState.WaitingForExecution:
                    result.State = LongRunningProcessState.Pending;
                    break;
                case Threading.ThreadState.Executed:
                    result.State = LongRunningProcessState.Executed;
                    break;
                case Threading.ThreadState.Error:
                    result.State = LongRunningProcessState.Error;
                    break;
                case Threading.ThreadState.WaitingForExternalData:
                    result.State = LongRunningProcessState.WaitingForExternalData;
                    break;
                case Threading.ThreadState.WaitingForUserInterface:
                    result.State = LongRunningProcessState.WaitingForUserInterface;
                    break;
            }

            return result;
        }

        public async Task<object?> GetProcessExternalDataAsync(string processNumber)
        {
            var processContainer = await _processInstanceContainerQueries.GetAll()
                .Include(x=> x.ProcessExternalData)
                .SingleAsync(x=>x.ProcessNumber == processNumber);

            return _fullSerializer.Deserialize<object>(processContainer.ProcessExternalData.ProcessExternalDataFullSerialization);
        }

        public async Task SetProcessExternalDataAsync(string processNumber, object? externalData)
        {
            var xml = _fullSerializer.Serialize(externalData);
            await _processInstanceContainerCommands.SetExternalDataAsync(processNumber, xml);                
        }

        public async Task<View?> GetProcessViewAsync(string processNumber)
        {
            return (View?)await GetProcessExternalDataAsync(processNumber);
        }

        public async Task SetProcessViewDataAsync(string processNumber, ViewResponse? viewResponse)
        {
            await SetProcessExternalDataAsync(processNumber, viewResponse);
        }

        internal async Task SaveProcessAsync(IProcess process)
        {
            await _processCommands.SaveProcessStateAsync(process);
        }
    }
}
