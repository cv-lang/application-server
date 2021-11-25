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
    public class ProcessInstanceRepository : Repository<ProcessInstance>
    {
        public ProcessInstanceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<ProcessInstance> GetSingleAsync(long id)
        {
            return await base.GetAll().Include(x=> x.ProcessInstanceStateData).SingleAsync(x=> x.Id == id);
        }
    }
}
