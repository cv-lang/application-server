using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Cvl.ApplicationServer.Processes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class ProcessStateDataService : BaseService<ProcessStateData, ProcessStateDataRepository>
    {
        private readonly IFullSerializer _fullSerializer;

        public ProcessStateDataService(ProcessStateDataRepository repository, IFullSerializer fullSerializer)
            : base(repository)
        {
            this._fullSerializer = fullSerializer;
        }

        public async Task<ProcessStateData> GetProcessStateByProcessId(long processId)
        {
            return await Repository.GetAll().SingleAsync(x=> x.ProcessInstanceId == processId);
        }

        public async Task SerializeProcessAsync(IProcess process)
        {
            if (process is IProcessSerialization processSerialization)
            {
                var json = processSerialization.ProcessSerizalization(_fullSerializer);
                var stateData = await GetProcessStateByProcessId(process.ProcessId);
                stateData.ModifiedDate = DateTime.Now;
                stateData.ProcessStateFullSerialization = json;
                await Repository.SaveChangesAsync();
            }
        }

        public void DeserializeProcess(IProcess process, string fullState)
        {
            if (process is IProcessSerialization processSerialization)
            {
                processSerialization.ProcessDeserialization(_fullSerializer, fullState);
            }
        }
    }
}
