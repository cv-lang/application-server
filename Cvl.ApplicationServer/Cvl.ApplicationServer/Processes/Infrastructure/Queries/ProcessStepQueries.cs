using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Repositories;

namespace Cvl.ApplicationServer.Core.Processes.Queries
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
