using OfflineOrderManager.Models.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfflineOrderManager.Services.Contracts
{
    public interface IEntityService
    {
        Task AddOrUpdate<TEntity>(TEntity model) where TEntity : IEntity;

        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity;

        IEnumerable<TEntity> GetAll<TEntity>(Func<TEntity, bool> predicate) where TEntity : class;

        TEntity GetBy<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;

        Task DeleteBy<TEntity>(Func<TEntity, bool> predicate) where TEntity : class;
    }
}
