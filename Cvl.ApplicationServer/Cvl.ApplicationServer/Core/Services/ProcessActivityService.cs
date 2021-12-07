using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class ProcessActivityService : BaseService<ProcessActivity, ProcessActivityRepository>
    {
        public ProcessActivityService(ProcessActivityRepository repository)
            : base(repository)
        { }

        public IQueryable<ProcessActivity> GetProcessActivities(long procesId)
        {
            return Repository.GetAll().Where(x => x.ProcessInstanceId == procesId);
        }
    }
}
