using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Processes.Dtos;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Services;

namespace Cvl.ApplicationServer.Server.Areas.ApplicationServer.Controllers
{
    [ApiController]
    [Route("api/applicationserver/[controller]")]
    public class ProcessesController : ControllerBase
    {
        private readonly IProcessesControllerService _processesControllerService;

        public ProcessesController(IProcessesControllerService processesControllerService)
        {
            _processesControllerService = processesControllerService;
        }

        [Route("Processes_Read")]
        public DataSourceResult Processes_Read([DataSourceRequest] DataSourceRequest request)
        {
            var objects = _processesControllerService.GetAllProcesses();
            return objects.ToDataSourceResult(request, x => new ProcessListItemDto(x));
        }

        [Route("ProcessActivities_Read")]
        public DataSourceResult ProcessActivities_Read([DataSourceRequest] DataSourceRequest request, long processId)
        {
            var objects = _processesControllerService.GetProcessActivities(processId);
            return objects.ToDataSourceResult(request, x=> new ProcessActivityDto(x));
        }

        [Route("ProcessSteps_Read")]
        public DataSourceResult ProcessSteps_Read([DataSourceRequest] DataSourceRequest request, long processId)
        {
            var objects = _processesControllerService.GetProcessSteps(processId);
            return objects.ToDataSourceResult(request, x=> new ProcessStepHistoryDto(x));
        }
    }
}
