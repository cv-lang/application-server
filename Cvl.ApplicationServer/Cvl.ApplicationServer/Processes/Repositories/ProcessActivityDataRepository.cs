using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model.Processes;

namespace Cvl.ApplicationServer.Core.Repositories
{
    public class ProcessActivityDataRepository : Repository<ProcessActivityData>
    {
        public ProcessActivityDataRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
