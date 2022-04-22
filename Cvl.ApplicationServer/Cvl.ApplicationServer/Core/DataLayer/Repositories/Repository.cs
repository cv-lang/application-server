using Cvl.ApplicationServer.Core.DataLayer.Model;
using Cvl.ApplicationServer.Core.DataLayer.Model.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cvl.ApplicationServer.Core.DataLayer.Repositories
{
    internal class Repository<T> : IRepository<T> where T : BaseEntity
    {
        #region property  
        private readonly ApplicationServerDbContext _applicationDbContext;
        private readonly DbSet<T> entities;
        #endregion

        #region Constructor  
        public Repository(ApplicationServerDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();
        }
        #endregion

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
        }

        public virtual Task<T> GetSingleAsync(long id)
        {
            return entities.SingleAsync(c => c.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return entities.Where(x=> x.Archival == false);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Add(entity);
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
        }

        public Task SaveChangesAsync()
        {
            return _applicationDbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Update(entity);
        }
    }
}