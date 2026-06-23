using System.Linq.Expressions;

namespace GoKinoGo.DataAccess.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void DeleteAsync(T entity);
    void Update(T entity);
}

