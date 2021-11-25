using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Services
{
    public class BaseService<T, TRepository> 
        where T : BaseEntity
        where TRepository : IRepository<T>
    {
        protected readonly TRepository Repository;

        public BaseService(TRepository repository)
        {
            Repository = repository;
        }

        public IQueryable<T> GetAllObjects()
        {
            return Repository.GetAll();
        }

        public async Task<T> GetSingleAsync(long processId)
        {
            return await Repository.GetSingleAsync(processId);
        }
    }
}
