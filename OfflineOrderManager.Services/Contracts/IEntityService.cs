using System;
using System.Threading.Tasks;

namespace OfflineOrderManager.Services.Contracts
{
    public interface IEntityService
    {
        Task Add<TEntity>(object model);

        TEntity Get<TEntity>(Func<TEntity, bool> predicate) where TEntity : class;

        Task Delete<TEntity>(Func<TEntity, bool> predicate) where TEntity : class;
    }
}
