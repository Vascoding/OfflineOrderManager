using OfflineOrderManager.Models.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfflineOrderManager.Services.Contracts
{
    public interface IEntityService
    {
        Task AddOrUpdate<TEntity>(TEntity model) where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;

        Task<TEntity> GetBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;

        Task DeleteBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity;
    }
}