﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Queries;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Processes.Services
{
    public interface IProcessesControllerService
    {
        IQueryable<ProcessInstanceContainer> GetAllProcesses();
        IQueryable<ProcessActivity> GetProcessActivities(long processId);
        IQueryable<ProcessStepHistory> GetProcessSteps(long processId);
        Task<ProcessInstanceContainer> GetProcessContainerAsync(string processNumber);
    }
    internal class ProcessesControllerService : IProcessesControllerService
    {
        private readonly ProcessInstanceContainerQueries _processInstanceContainerQueries;
        private readonly ProcessActivityQueries _processActivityQueries;
        private readonly ProcessStepQueries _processStepQueries;

        public ProcessesControllerService(ProcessInstanceContainerQueries processInstanceContainerQueries,
            ProcessActivityQueries processActivityQueries,
            ProcessStepQueries processStepQueries)
        {
            _processInstanceContainerQueries = processInstanceContainerQueries;
            _processActivityQueries = processActivityQueries;
            _processStepQueries = processStepQueries;
        }

        public IQueryable<ProcessInstanceContainer> GetAllProcesses()
        {
            return _processInstanceContainerQueries.GetAllWithDiagnosticeAndExternal()
                .OrderByDescending(x=> x.Id);
        }

        public IQueryable<ProcessActivity> GetProcessActivities(long processId)
        {
            return _processActivityQueries.GetAll()
                .Where(x=> x.ProcessInstanceId == processId);
        }

        public IQueryable<ProcessStepHistory> GetProcessSteps(long processId)
        {
            return _processStepQueries.GetAll()
                .Where(x => x.ProcessInstanceContainerId == processId)
                .OrderByDescending(x=>x.Id);
        }

        public async Task<ProcessInstanceContainer> GetProcessContainerAsync(string processNumber)
        {
            return await _processInstanceContainerQueries
                .GetProcessInstanceContainerByProcessNumberAsync(processNumber);
        }
    }
}
