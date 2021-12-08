using Cvl.ApplicationServer.Core.Model.Temporary;
using Cvl.ApplicationServer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class LogElementService : BaseService<LogElement, LogElementRepository>
    {
        public LogElementService(LogElementRepository repository)
            : base(repository)
        { }
    }
}
