using System;
using System.Linq;
using System.Threading.Tasks;
using OfflineOrderManager.Data;
using OfflineOrderManager.Services.Contracts;

namespace OfflineOrderManager.Services.Implementations
{
    public class EntityService : IEntityService
    {
        private readonly OfflineOrderManagerDbContext dbContext;
        private readonly IMappingService mapper;

        public EntityService(OfflineOrderManagerDbContext dbContext, IMappingService mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public TEntity Get<TEntity>(Func<TEntity, bool> predicate) where TEntity : class => 
            this.dbContext.Set<TEntity>()
                .FirstOrDefault(predicate);

        public async Task Add<Т>(object model)
        {
            var entity = this.mapper.Map<Т>(model);

            await dbContext.AddAsync(entity);

            await dbContext.SaveChangesAsync();
        }

        public async Task Delete<TEntity>(Func<TEntity, bool> predicate) where TEntity : class
        {
            var entity = this.dbContext.Set<TEntity>().FirstOrDefault(predicate);

            this.dbContext.Remove(entity);

            await dbContext.SaveChangesAsync();
        }
    }
}
