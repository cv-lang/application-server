using Cvl.ApplicationServer.Core.Model.Processes;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Server.Areas.ApplicationServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessesController : ControllerBase
    {
        private readonly Core.ApplicationServerOld _applicationServer;

        public ProcessesController(Core.ApplicationServerOld applicationServer)
        {
            this._applicationServer = applicationServer;
        }

        [Route("Processes_Read")]
        public DataSourceResult Processes_Read([DataSourceRequest] DataSourceRequest request)
        {
            var objects = _applicationServer.Processes.GetAllProcesses();
            return objects.ToDataSourceResult(request);
        }

        [Route("ProcessActivities_Read")]
        public DataSourceResult ProcessActivities_Read([DataSourceRequest] DataSourceRequest request, long processId)
        {
            var objects = _applicationServer.Processes.GetProcessActivities(processId);
            return objects.ToDataSourceResult(request);
        }

        [Route("ProcessSteps_Read")]
        public DataSourceResult ProcessSteps_Read([DataSourceRequest] DataSourceRequest request, long processId)
        {
            var objects = _applicationServer.Processes.GetProcessSteps(processId);
            return objects.ToDataSourceResult(request);
        }

        //TODO: do usuniecia
        //public async Task<ProcessInstanceContainer> GetProcessInstanceContainer(long processId)
        //{
        //    return await _applicationServer.Processes.GetProcessInstanceContainerAsync(processId);
        //}


    }
}
