
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepo<TEntity, TContext> where TEntity : class where TContext : DataContext
{
    private readonly DataContext _context;

    protected BaseRepo(DataContext context)
    {
        _context = context;
    }
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR CreateAsync::" + ex.Message);
        }
        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            if (entities != null)
            {
                return entities;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetAllAsync::" + ex.Message);
        }
        return null!;
    }

    public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetOneAsync::" + ex.Message);
        }
        return null!;
    }

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
    {
        try
        {
            var existing = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();

                return existing;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR UpdateAsync::" + ex.Message);
        }
        return null!;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR Delete :: " + ex.Message);
        }
        return false;
    }

    public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entityFound = await _context.Set<TEntity>().AnyAsync(expression);
            return entityFound;


        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR ExistAsync:: " + ex.Message);
        }
        return false;
    }
}








