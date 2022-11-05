using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Belle.Services.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<T> Get(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task SaveChanges();
    }
}