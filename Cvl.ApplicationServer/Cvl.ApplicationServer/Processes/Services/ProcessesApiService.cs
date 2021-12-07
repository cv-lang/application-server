using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Processes.Dtos;
using Cvl.ApplicationServer.Processes.Services.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Services
{
    public class ProcessesApiService
    {
        private readonly ProcessInstanceContainerService _processInstanceService;
        private readonly ProcessActivityService _processActivityService;
        private readonly ProcessStepHistoryService _processStepHistoryService;

        public ProcessesApiService(ProcessInstanceContainerService processInstanceService,
            ProcessActivityService processActivityService,
            ProcessStepHistoryService processStepHistoryService)
        {
            this._processInstanceService = processInstanceService;
            this._processActivityService = processActivityService;
            this._processStepHistoryService = processStepHistoryService;
        }


        public IQueryable<ProcessListItemDto> GetAllProcesses()
        {
            var list = _processInstanceService.GetAllObjects().OrderByDescending(x=> x.Id);

            var listDto = list.Include(x => x.ProcessDiagnosticData).Select(x => new ProcessListItemDto(x));
            return listDto;
        }

        public IQueryable<ProcessActivityDto> GetProcessActivities(long processId)
        {
            var list = _processActivityService.GetProcessActivities(processId).OrderByDescending(x => x.Id);

            var listDto = list.Select(x => new ProcessActivityDto(x));
            return listDto;
        }

        public IQueryable<ProcessStepHistoryDto> GetProcessSteps(long processId)
        {
            var list = _processStepHistoryService.GetProcessSteps(processId).OrderByDescending(x => x.Id);

            var listDto = list.Select(x => new ProcessStepHistoryDto(x));
            return listDto;
        }
    }
}
