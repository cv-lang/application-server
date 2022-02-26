using Cvl.ApplicationServer.Core.Model.Temporary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model.Contexts;

namespace Cvl.ApplicationServer.Core.Repositories
{
    internal class LogElementRepository : Repository<LogElement>
    {
        public LogElementRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
