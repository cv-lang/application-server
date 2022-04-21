using Cvl.ApplicationServer.Core.Model.Contexts;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Processes.Repositories
{
    internal class ProcessStateDataRepository : Repository<ProcessStateData>
    {
        public ProcessStateDataRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<ProcessStateData> GetProcessInstanceStateDataByProcessIdAsync(long processId)
        {
            return await GetAll().SingleAsync(x => x.ProcessInstanceId == processId);
        }
    }
}
