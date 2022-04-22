using Cvl.ApplicationServer.Processes.Core.Model;
using Cvl.ApplicationServer.Processes.Core.Repositories;

namespace Cvl.ApplicationServer.Processes.Core.Queries
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
