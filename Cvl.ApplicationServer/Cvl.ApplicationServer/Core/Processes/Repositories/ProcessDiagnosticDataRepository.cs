using Cvl.ApplicationServer.Core.Model.Contexts;
using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Repositories;

namespace Cvl.ApplicationServer.Core.Processes.Repositories
{
    internal class ProcessDiagnosticDataRepository : Repository<ProcessDiagnosticData>
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
