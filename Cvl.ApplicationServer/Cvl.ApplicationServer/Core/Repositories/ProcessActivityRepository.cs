using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model;

namespace Cvl.ApplicationServer.Core.Repositories
{
    public class ProcessActivityRepository : Repository<ProcessActivity>
    {
        public ProcessActivityRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
