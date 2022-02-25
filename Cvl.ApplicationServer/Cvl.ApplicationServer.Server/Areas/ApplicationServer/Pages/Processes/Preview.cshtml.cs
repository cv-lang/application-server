using Cvl.ApplicationServer.Core.Serializers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.ApplicationServer.Server.Areas.ApplicationServer.Pages.Processes
{
    public class PreviewModel : PageModel
    {
        private readonly IApplicationServer _applicationServer;
        private readonly ISerializer _serializer;

        public PreviewModel(IApplicationServer applicationServer, IJsonSerializer serializer)
        {
            this._applicationServer = applicationServer;
            this._serializer = serializer;
        }

        public string ProcessInstanceContainer { get; set; }
        public string ProcessState { get; set; }
        public string ProcessNumber { get; set; }
        public long ProcessId { get; set; }

        public async Task OnGet(string processNumber)
        {
            ProcessNumber = processNumber;
            var process = _applicationServer.Processes.LoadProcess(processNumber);
            if (process == null)
            {
                throw new Exception($"There is no process with processId={processNumber}");
            }

            ProcessInstanceContainer = _serializer.Serialize(process.ProcessData.ProcessInstanceContainer);
            ProcessId = process.ProcessData.ProcessId;


            //remove $type from full serialization
            ProcessInstanceContainer = ProcessInstanceContainer
                .Replace("\\\"","`").Replace("\r","").Replace("\n","")
                .Replace("\"{", "{").Replace("}\"","}").Replace("\\","");
                
            ProcessState = _serializer.Serialize(process);
           ProcessState = ProcessState
               .Replace("\\\"", "`").Replace("\r", "").Replace("\n", "")
               .Replace("\"{", "{").Replace("}\"", "}").Replace("\\", "");


        }
    }
}
