using Cvl.ApplicationServer.Core.DataLayer.Model.Contexts;
using Cvl.ApplicationServer.Core.DataLayer.Repositories;
using Cvl.ApplicationServer.Processes.Core.Model;

namespace Cvl.ApplicationServer.Processes.Core.Repositories
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
