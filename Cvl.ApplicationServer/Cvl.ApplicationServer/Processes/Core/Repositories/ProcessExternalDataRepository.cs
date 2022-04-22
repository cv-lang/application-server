using Cvl.ApplicationServer.Core.DataLayer.Model.Contexts;
using Cvl.ApplicationServer.Core.DataLayer.Repositories;
using Cvl.ApplicationServer.Processes.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Core.Repositories
{
    internal class ProcessExternalDataRepository : Repository<ProcessExternalData>
    {
        public ProcessExternalDataRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<ProcessExternalData> GetProcessExternalDataByProcessIdAsync(long processId)
        {
            return await GetAll().SingleAsync(x => x.ProcessInstanceId == processId);
        }
    }
}
