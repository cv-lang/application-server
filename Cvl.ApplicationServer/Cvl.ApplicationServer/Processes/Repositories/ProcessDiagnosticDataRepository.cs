using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Repositories
{
    public class ProcessDiagnosticDataRepository : Repository<ProcessDiagnosticData>
    {
        public ProcessDiagnosticDataRepository(ApplicationServerDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public ProcessDiagnosticData GetByProcessId(long processId)
        {
            return this.GetAll().Single(x=> x.ProcessInstanceId == processId);
        }
    }
}
