using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model.Processes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Repositories
{
    public class ProcessInstanceStateDataRepository : Repository<ProcessStateData>
    {
        public ProcessInstanceStateDataRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<ProcessStateData> GetProcessInstanceStateDataByProcessIdAsync(long processId)
        {
            return await GetAll().SingleAsync(x => x.ProcessInstanceId == processId);
        }
    }
}
