using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model.Processes;

namespace Cvl.ApplicationServer.Core.Repositories
{
    public class ProcessActivityRepository : Repository<ProcessActivity>
    {
        public ProcessActivityRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
