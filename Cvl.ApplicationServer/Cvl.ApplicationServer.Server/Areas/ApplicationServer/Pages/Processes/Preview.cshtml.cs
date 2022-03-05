using Cvl.ApplicationServer.Core.Processes;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Services;
using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.ApplicationServer.Server.Areas.ApplicationServer.Pages.Processes
{
    public class PreviewModel : PageModel
    {
        private readonly IProcessesControllerService _processesControllerService;
        private readonly ISerializer _serializer;

        public PreviewModel(IProcessesControllerService processesControllerService, IFullSerializer serializer)
        {
            _processesControllerService = processesControllerService;
            _serializer = serializer;
        }

        public string ProcessInstanceContainer { get; set; }
        public string ProcessState { get; set; }
        public string ProcessNumber { get; set; }
        public long ProcessId { get; set; }

        public async Task OnGet(string processNumber)
        {
            ProcessNumber = processNumber;

            var process = await _processesControllerService.GetProcessContainerAsync(processNumber);
           
            if (process == null)
            {
                throw new Exception($"There is no process with processId={processNumber}");
            }

            
            ProcessId = process.Id;


            
            ProcessState = process.ProcessInstanceStateData.ProcessStateFullSerialization;

            process.ProcessInstanceStateData = new ProcessStateData(string.Empty);
            ProcessInstanceContainer = _serializer.Serialize(process);
        }
    }
}
