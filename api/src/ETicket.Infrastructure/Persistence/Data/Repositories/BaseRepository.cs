using System.Linq.Expressions;
using ETicket.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace ETicket.Infrastructure.Persistence.Data.Repositories;

public class BaseRepository<T>(ApplicationDbContext applicationDbContext) : IRepository<T>
    where T : Entity, IAggregateRoot
{
    private readonly DbSet<T> _dbSet = applicationDbContext.Set<T>();

    public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null)
    {
        var query = _dbSet.AsQueryable();

        if (filter is not null)
            query = query.Where(filter);

        return query;
    }

    public void Update(T entity)
    {
        _dbSet.Entry(entity).State = EntityState.Modified;
        _dbSet.Update(entity);
    }

    public async Task Delete(long id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is not null)
            _dbSet.Remove(entity);
    }

    public async Task<T> GetById(long id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public void Add(T entity)
        =>_dbSet.Add(entity);

    public void AddMany(List<T> entities)
        => _dbSet.AddRange(entities);
}