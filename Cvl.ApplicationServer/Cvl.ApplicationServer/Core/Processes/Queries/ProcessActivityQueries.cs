using Cvl.ApplicationServer.Core.Processes.Model;
using Cvl.ApplicationServer.Core.Processes.Repositories;

namespace Cvl.ApplicationServer.Core.Processes.Queries
{
    internal class ProcessActivityQueries
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
