using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Repositories;

namespace Cvl.ApplicationServer.Processes.Queries
{
    public class ProcessStepQueries
    {
        private readonly ProcessStepHistoryRepository _processStepHistoryRepository;

        public ProcessStepQueries(ProcessStepHistoryRepository processStepHistoryRepository)
        {
            _processStepHistoryRepository = processStepHistoryRepository;
        }
        public IQueryable<ProcessStepHistory> GetAll()
        {
            return _processStepHistoryRepository.GetAll();
        }
    }
}
