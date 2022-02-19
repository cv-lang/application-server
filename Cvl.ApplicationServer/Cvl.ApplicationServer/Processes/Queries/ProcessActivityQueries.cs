using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model.Processes;
using Cvl.ApplicationServer.Core.Repositories;

namespace Cvl.ApplicationServer.Processes.Queries
{
    public class ProcessActivityQueries
    {
        private readonly ProcessActivityRepository _processActivityRepository;

        public ProcessActivityQueries(ProcessActivityRepository processActivityRepository)
        {
            _processActivityRepository = processActivityRepository;
        }
        public IQueryable<ProcessActivity> GetAll()
        {
            return _processActivityRepository.GetAll();
        }
    }
}
