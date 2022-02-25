using Cvl.ApplicationServer.Core.Model.Contexts;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Processes.Repositories
{
    public class ProcessExternalDataRepository : Repository<ProcessExternalData>
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
