using Cvl.ApplicationServer.Core.DataLayer.Model.Contexts;
using Cvl.ApplicationServer.Core.DataLayer.Model.Temporary;

namespace Cvl.ApplicationServer.Core.DataLayer.Repositories
{
    internal class LogElementRepository : Repository<LogElement>
    {
        public LogElementRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
