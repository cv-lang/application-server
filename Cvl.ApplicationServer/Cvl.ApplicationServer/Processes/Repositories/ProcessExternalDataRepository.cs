using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Repositories;
using Cvl.ApplicationServer.Processes.Model;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Repositories
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
