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
        private readonly Core.ApplicationServer _applicationServer;

        public ProcessesController(Core.ApplicationServer applicationServer)
        {
            this._applicationServer = applicationServer;
        }

        [Route("Processes_Read")]
        public DataSourceResult Processes_Read([DataSourceRequest] DataSourceRequest request)
        {
            var objects = _applicationServer.Processes.GetAllProcesses();
            return objects.ToDataSourceResult(request);
        }
    }
}
