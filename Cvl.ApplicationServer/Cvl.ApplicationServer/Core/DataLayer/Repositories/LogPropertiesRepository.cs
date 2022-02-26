using Cvl.ApplicationServer.Core.Model.Temporary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model.Contexts;

namespace Cvl.ApplicationServer.Core.Repositories
{
    internal class LogPropertiesRepository : Repository<LogProperties>
    {
        public LogPropertiesRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
