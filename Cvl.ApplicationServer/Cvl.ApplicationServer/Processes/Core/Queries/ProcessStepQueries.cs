using Cvl.ApplicationServer.Processes.Core.Model;
using Cvl.ApplicationServer.Processes.Core.Repositories;

namespace Cvl.ApplicationServer.Processes.Core.Queries
{
    internal class ProcessStepQueries
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
