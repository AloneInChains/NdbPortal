using NdbPortal.Contracts;
using NdbPortal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace NdbPortal.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected NDBContext RepositoryContext { get; set; }
        public RepositoryBase(NDBContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public IQueryable<T> GetAll() => RepositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> GetWithWhere(Expression<Func<T, bool>> expression) =>
            RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}