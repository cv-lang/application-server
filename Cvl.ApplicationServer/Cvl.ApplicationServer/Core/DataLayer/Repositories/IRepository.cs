using Cvl.ApplicationServer.Core.DataLayer.Model;

namespace Cvl.ApplicationServer.Core.DataLayer.Repositories
{
    internal interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetSingleAsync(long Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}
