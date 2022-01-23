using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model.Temporary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Repositories
{
    public class LogPropertiesRepository : Repository<LogProperties>
    {
        public LogPropertiesRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
