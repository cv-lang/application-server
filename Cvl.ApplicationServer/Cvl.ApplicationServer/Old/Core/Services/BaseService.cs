using Cvl.ApplicationServer.Core.Database.Contexts;
using Cvl.ApplicationServer.Core.Model;
using Cvl.ApplicationServer.Core.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<T> GetSingleAsync(long processId, bool loadNestedObject = false)
        {            
            return await Repository.GetSingleAsync(processId);
        }

        public async Task UpdateAsync(T entity)
        {
            Repository.Update(entity);
            await Repository.SaveChangesAsync();
        }

        public async Task InsertAsync(T entity)
        {
            Repository.Insert(entity);
            await Repository.SaveChangesAsync();
        }
    }
}
