﻿using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Interfaces;

namespace Authentication.Application.Services;

public abstract class CrudService<TEntity, TCreateDto, TUpdateDto>
    : ICrudService<TEntity, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
    where TCreateDto : class
    where TUpdateDto : class
{
    protected readonly ICrudRepository<TEntity> Repository;

    protected CrudService(ICrudRepository<TEntity> repository)
    {
        Repository = repository;
    }

    public virtual IQueryable<TEntity> Set()
    {
        return Repository.Set();
    }

    public virtual async Task<IEnumerable<TEntity?>> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public abstract Task<TEntity?> CreateAsync(TCreateDto createDto);

    public abstract Task<TEntity?> UpdateAsync(Guid id, TUpdateDto updateDto);

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await Repository.DeleteAsync(id);
    }

    public async Task<bool> RestoreAsync(Guid id)
    {
        return await Repository.RestoreAsync(id);
    }

    public virtual async Task<bool> ForceDeleteAsync(Guid id)
    {
        return await Repository.ForceDeleteAsync(id);
    }
}