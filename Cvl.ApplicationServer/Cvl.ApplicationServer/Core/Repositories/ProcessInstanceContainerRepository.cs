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
    public class ProcessInstanceContainerRepository : Repository<ProcessInstanceContainer>
    {
        public ProcessInstanceContainerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<ProcessInstanceContainer> GetSingleAsync(long processId)
        {
            return await base.GetAll().SingleAsync(x=> x.Id == processId);
        }

        public async Task<ProcessInstanceContainer> GetSingleByNumberAsync(string processNumber)
        {
            return await base.GetAll().SingleAsync(x => x.ProcessNumber == processNumber);
        }
    }
}
