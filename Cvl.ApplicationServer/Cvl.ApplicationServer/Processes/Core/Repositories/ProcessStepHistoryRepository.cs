using Cvl.ApplicationServer.Core.Model.Contexts;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Repositories;

namespace Cvl.ApplicationServer.Core.Processes.Repositories
{
    internal class ProcessStepHistoryRepository : Repository<ProcessStepHistory>
    {
        public ProcessStepHistoryRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
