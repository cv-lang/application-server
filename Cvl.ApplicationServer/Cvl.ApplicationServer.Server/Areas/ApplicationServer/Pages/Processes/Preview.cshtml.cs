using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.ApplicationServer.Server.Areas.ApplicationServer.Pages.Processes
{
    public class PreviewModel : PageModel
    {
        private readonly Core.ApplicationServer _applicationServer;
        private readonly ISerializer _serializer;

        public PreviewModel(Core.ApplicationServer applicationServer, IJsonSerializer serializer)
        {
            this._applicationServer = applicationServer;
            this._serializer = serializer;
        }

        public string ProcessInstanceContainer { get; set; }
        public string ProcessState { get; set; }
        public long ProcessId { get; set; }

        public async Task OnGet(long processId)
        {
            ProcessId = processId;
            var processContainer = await _applicationServer.Processes.GetProcessInstanceContainerWithNestedObject(processId);
            if (processContainer == null)
            {
                throw new Exception($"There is no process with processId={processId}");
            }

            ProcessState = processContainer.ProcessInstanceStateData.ProcessStateFullSerialization;

            //w serializacji kontenera nie chcemy stanu procesu
            processContainer.ProcessInstanceStateData.ProcessStateFullSerialization = string.Empty;
            ProcessInstanceContainer = _serializer.Serialize(processContainer);



            //remove $type from full serialization
            ProcessInstanceContainer = ProcessInstanceContainer
                .Replace("\"{", "{").Replace("}\"","}").Replace("\\","");

            ProcessState = ProcessState
                .Replace("\"{", "{").Replace("}\"", "}").Replace("\\", "");

        }
    }
}
