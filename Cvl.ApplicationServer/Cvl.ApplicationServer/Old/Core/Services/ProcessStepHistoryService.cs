using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class ProcessStepHistoryService : BaseService<ProcessStepHistory, ProcessStepHistoryRepository>
    {
        public ProcessStepHistoryService(ProcessStepHistoryRepository repository)
            : base(repository)
        { }

        public IQueryable<ProcessStepHistory> GetProcessSteps(long procesId)
        {
            return Repository.GetAll().Where(x => x.ProcessInstanceContainerId == procesId);
        }
    }
}
