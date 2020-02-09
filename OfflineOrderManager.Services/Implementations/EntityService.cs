using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfflineOrderManager.Data;
using OfflineOrderManager.Models.Data.Contracts;
using OfflineOrderManager.Services.Contracts;

namespace OfflineOrderManager.Services.Implementations
{
    public class EntityService : IEntityService
    {
        private readonly OfflineOrderManagerDbContext dbContext;

        public EntityService(OfflineOrderManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddOrUpdate<ТEntity>(ТEntity entity) where ТEntity : IEntity
        {
            dbContext.Update(entity);

            dbContext.SaveChanges();
        }

        public void DeleteBy<TEntity>(Func<TEntity, bool> predicate) where TEntity : class
        {
            var entity = this.dbContext.Set<TEntity>().FirstOrDefault(predicate);

            this.dbContext.Remove(entity);

            dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity => 
            this.dbContext.Set<TEntity>()
                .ToList();

        public IEnumerable<TEntity> GetAll<TEntity>(Func<TEntity, bool> predicate) where TEntity : class => 
            this.dbContext.Set<TEntity>()
                .Where(predicate)
                .ToList();

        public TEntity GetBy<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity =>
            this.dbContext.Set<TEntity>()
                .FirstOrDefault(predicate);
    }
}
