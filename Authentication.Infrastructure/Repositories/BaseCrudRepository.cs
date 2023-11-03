﻿using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class BaseCrudRepository<TEntity> : IBaseCrudRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context;

    public BaseCrudRepository(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity?>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> CreateAsync(TEntity? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _context.Set<TEntity>().Add(entity);

        _context.Entry(entity).State = EntityState.Added;

        return await SaveAsyncChanges(entity);
    }

    public virtual async Task<TEntity?> UpdateAsync(TEntity? entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _context.Entry(entity).State = EntityState.Modified;

        return await SaveAsyncChanges(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null) return false;
        entity.DeletedAt = DateTime.UtcNow;
        return await SaveAsyncChanges();
    }

    public async Task<bool> RestoreAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null) return false;
        entity.DeletedAt = null;
        return await SaveAsyncChanges();
    }

    public virtual async Task<bool> ForceDeleteAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);

        if (entity == null)
            return false;

        _context.Set<TEntity>().Remove(entity);

        return await SaveAsyncChanges();
    }

    private async Task<bool> SaveAsyncChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<TEntity?> SaveAsyncChanges(TEntity entity)
    {
        return await _context.SaveChangesAsync() > 0 ? entity : null;
    }
}