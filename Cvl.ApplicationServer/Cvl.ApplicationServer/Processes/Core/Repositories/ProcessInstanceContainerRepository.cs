using Cvl.ApplicationServer.Core.DataLayer.Model.Contexts;
using Cvl.ApplicationServer.Core.DataLayer.Repositories;
using Cvl.ApplicationServer.Processes.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Processes.Core.Repositories
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
