using System.Linq.Expressions;

namespace ETicket.SharedKernel;

public interface IRepository<T> where T : class, IAggregateRoot
{
    IQueryable<T> Get(Expression<Func<T, bool>>? filter = null);
    void Update(T entity);
    Task Delete(long id);
    Task<T> GetById(long id);
    void Add(T entity);
    void AddMany(List<T> entities);
}