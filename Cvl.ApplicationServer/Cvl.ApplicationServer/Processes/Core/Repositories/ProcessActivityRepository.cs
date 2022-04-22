using Cvl.ApplicationServer.Core.DataLayer.Model.Contexts;
using Cvl.ApplicationServer.Core.DataLayer.Repositories;
using Cvl.ApplicationServer.Processes.Core.Model;

namespace Cvl.ApplicationServer.Processes.Core.Repositories
{
    internal class ProcessActivityRepository : Repository<ProcessActivity>
    {
        public ProcessActivityRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
