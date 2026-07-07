namespace XBLog.Domain.Contract;

public interface IBaseRepository<T> where T : class
{
    Task<T> FindByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}
