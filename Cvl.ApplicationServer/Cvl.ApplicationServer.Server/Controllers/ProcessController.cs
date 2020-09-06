using Cvl.ApplicationServer.Server.Extensions;
using Cvl.ApplicationServer.Server.Node.Processes.Interfaces;
using Cvl.ApplicationServer.Server.Node.Processes.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Server.Controllers
{
    public class ProcessController : Controller
    {
        public static IProcessEngine ProcessEngine
        {
            get
            {
                return ApplicationServerExtensions.ApplicationServerNodeHost.ProcessManager;
            }
        }

        [HttpPost("process/{id}")]
        [HttpGet("process/{id}")]
        public IActionResult Index(int id)
        {
            //var id = 2;

            var desc = ProcessEngine.GetProcessData((long)id);
            object model = null;
            string viewName = "";

            switch (desc.ProcessStatus)
            {
                case EnumProcessStatus.Error:
                    viewName = "GeneralView";
                    model = new GeneralViewModel() { Header = "Błąd procesu", Content = "bababa" };
                    break;
                case EnumProcessStatus.Executed:
                    if (desc.FormData != null)
                    {
                        viewName = desc.FormData.FormName;
                        model = desc.FormData.FormModel;
                    }
                    else
                    {
                        viewName = "GeneralView";
                        model = new GeneralViewModel() { Header = "Proces został zakońcony", Content = "Proces został zakończony" };
                    }
                    break;
                case EnumProcessStatus.WaitingForExecution:
                case EnumProcessStatus.WaitingForHost:
                case EnumProcessStatus.Running:
                    viewName = desc.WaitingFormData.FormName;
                    model = desc.WaitingFormData.FormModel;
                    break;
                case EnumProcessStatus.WaitingForUserData:
                    viewName = desc.FormData.FormName;
                    model = desc.FormData.FormModel;
                    break;


            }
            return View(viewName, model);
        }


        public IActionResult StartProcess(string processType)
        {
            if (string.IsNullOrEmpty(processType))
            {
                processType = "Smeo.BusinessProcesses.Jdg.JdgProcess";
            }
            var id = ProcessEngine.StartProcess(processType);
            return Redirect($"{id}");
        }


        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> SetProcessData(long ProcessId)
        {
            var process = ProcessController.ProcessEngine.GetProcessData(ProcessId);
            var modelType = process.FormData.FormModel.GetType();
            var model = (FormModel)Activator.CreateInstance(modelType);
            //var model = new RegistrationModel();


            MethodInfo method = typeof(Controller).GetMethods()
                .Where(x => x.Name == "TryUpdateModelAsync").First();//.GetMethod("TryUpdateModelAsync", new[] { typeof(object) });
            MethodInfo generic = method.MakeGenericMethod(modelType);
            Task<bool> result = (Task<bool>)generic.Invoke(this, new object[] { model });

            var t = await result;

            var clientIP = HttpContext.Connection.RemoteIpAddress.ToString();
            model.ClientIpAddress = clientIP;

            ProcessController.ProcessEngine.SetProcessData(model);
            return Redirect($"/Process/{model.ProcessId}");
        }
    }
}
