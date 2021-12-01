using Cvl.ApplicationServer.Core.Services;
using Cvl.ApplicationServer.Processes.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Services
{
    public class ProcessService
    {
        private readonly ProcessInstanceContainerService _processInstanceService;

        public ProcessService(ProcessInstanceContainerService processInstanceService)
        {
            this._processInstanceService = processInstanceService;
        }


        public IQueryable<ProcessListItemDto> GetAllProcesses()
        {
            var list = _processInstanceService.GetAllObjects().OrderByDescending(x=> x.Id);

            var listDto = list.Include(x => x.ProcessDiagnosticData).Select(x => new ProcessListItemDto(x));
            return listDto;
        }
    }
}
