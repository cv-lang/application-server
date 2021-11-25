using Cvl.ApplicationServer.Core;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using Cvl.ApplicationServer.Processes.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Base
{
    [ThreadType(ThreadType = ThreadType.Multithreaded)]
    public abstract class BaseProcess : IProcess, IProcessSerialization
    {
        private Core.ApplicationServer _applicationServer;

        public BaseProcess(Core.ApplicationServer applicationServer)
        {
            _applicationServer = applicationServer;
        }

        public long ProcessId { get; set; }

        public abstract void ProcessDeserialization(IFullSerializer serializer, string serializedProcess);

        public abstract string ProcessSerizalization(IFullSerializer serializer);

        protected async Task SetStepAsync(string stepName, string description, int? step = null)
        {
            await _applicationServer.SetStepAsync(ProcessId, stepName, description, step);
        }
    }
}
