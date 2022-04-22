using Cvl.ApplicationServer.Core.DataLayer.Model.Contexts;
using Cvl.ApplicationServer.Core.DataLayer.Model.Temporary;

namespace Cvl.ApplicationServer.Core.DataLayer.Repositories
{
    internal class LogPropertiesRepository : Repository<LogProperties>
    {
        public LogPropertiesRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
