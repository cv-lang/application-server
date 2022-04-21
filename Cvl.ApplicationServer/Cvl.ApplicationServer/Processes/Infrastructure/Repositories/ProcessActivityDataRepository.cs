using Cvl.ApplicationServer.Core.Model.Contexts;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Repositories;

namespace Cvl.ApplicationServer.Core.Processes.Repositories
{
    internal class ProcessActivityDataRepository : Repository<ProcessActivityData>
    {
        public ProcessActivityDataRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
