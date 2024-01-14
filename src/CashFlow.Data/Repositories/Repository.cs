using System.Linq.Expressions;
using CashFlow.Data.Context;
using CashFlow.Domain.BaseDefinitions;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Data.Repositories;

public class Repository<T> where T : Entity
{
    protected readonly CashFlowContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(CashFlowContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> predicate)
    {
        var query = _dbSet.AsQueryable();
        return await query.Where(predicate).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var query = _dbSet.AsQueryable();
        return await query.SingleOrDefaultAsync(c => c.Id == id);
    }

    public void Insert(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        if (entity.Id == 0) return;
        var etty = _dbSet.FirstOrDefault(t => t.Id == entity.Id);
        if (etty! != null!)
            _context.Entry(etty).CurrentValues.SetValues(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}