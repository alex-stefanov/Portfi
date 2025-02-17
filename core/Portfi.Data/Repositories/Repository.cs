﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Portfi.Data.Repositories;

/// <summary>
/// A generic repository implementation for data access operations.
/// </summary>
/// <typeparam name="TType">The type of the entity.</typeparam>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
public class Repository<TType, TId>(
    PortfiDbContext dbContext)
    : IRepository<TType, TId>
    where TType : class
{
    private readonly DbSet<TType> dbSet
        = dbContext.Set<TType>();

    /// <inheritdoc />
    public void Add(
        TType item)
    {
        dbSet.Add(item);
        dbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task AddAsync(
        TType item)
    {
        await dbSet.AddAsync(item);
        await dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public void AddRange(
        TType[] items)
    {
        dbSet.AddRange(items);
        dbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task AddRangeAsync(
        TType[] items)
    {
        await dbSet.AddRangeAsync(items);
        await dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public bool Delete(
        TType entity)
    {
        dbSet.Remove(entity);
        int changes = dbContext.SaveChanges();

        return changes > 0;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(
        TType entity)
    {
        dbSet.Remove(entity);
        int changes = await dbContext.SaveChangesAsync();

        return changes > 0;
    }

    /// <inheritdoc />
    public TType? FirstOrDefault(
        Func<TType, bool> predicate)
    {
        TType? entity = dbSet
            .FirstOrDefault(predicate);

        return entity;
    }

    /// <inheritdoc />
    public async Task<TType?> FirstOrDefaultAsync(
        Expression<Func<TType, bool>> predicate)
    {
        TType? entity = await dbSet
             .FirstOrDefaultAsync(predicate);

        return entity;
    }

    /// <inheritdoc />
    public IEnumerable<TType> GetAll()
        => [.. dbSet];

    /// <inheritdoc />
    public async Task<IEnumerable<TType>> GetAllAsync()
        => await dbSet.ToArrayAsync();

    /// <inheritdoc />
    public IQueryable<TType> GetAllAttached()
        => dbSet.AsQueryable();

    /// <inheritdoc />
    public TType? GetById(
        TId id)
    {
        TType? entity = dbSet
            .Find(id);

        return entity;
    }

    /// <inheritdoc />
    public async Task<TType?> GetByIdAsync(
        TId id)
    {
        TType? entity = await dbSet
            .FindAsync(id);

        return entity;
    }

    /// <inheritdoc />
    public bool Update(
        TType item)
    {
        try
        {
            dbSet.Attach(item);
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(
        TType item)
    {
        try
        {
            dbSet.Attach(item);
            dbContext.Entry(item).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}