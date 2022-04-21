using Cvl.ApplicationServer.Core.Model.Contexts;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.Processes.Repositories
{
    internal class ProcessInstanceContainerRepository : Repository<ProcessInstanceContainer>
    {
        public ProcessInstanceContainerRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
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
