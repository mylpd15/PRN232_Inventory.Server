using System.Linq.Expressions;

namespace WareSync.Repositories;
public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(object id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    void Remove(T entity);
    IQueryable<T> Query();
    Task SaveChangesAsync();
} 