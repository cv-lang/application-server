using Cvl.ApplicationServer.Core.DataLayer.Model.Contexts;
using Cvl.ApplicationServer.Core.DataLayer.Repositories;
using Cvl.ApplicationServer.Processes.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Core.Repositories
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
