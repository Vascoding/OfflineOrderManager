using System.Linq;
using System.Threading.Tasks;
using OfflineOrderManager.Data;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Models.Services.Orders;
using OfflineOrderManager.Services.Contracts;

namespace OfflineOrderManager.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly OfflineOrderManagerDbContext dbContext;
        private readonly IMappingService mapper;
        private readonly IUserService userService;

        public OrderService(OfflineOrderManagerDbContext dbContext, IMappingService mapper, IUserService userService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task Add(string userName, OrderServiceModel model)
        {
            var user = this.dbContext.Users.FirstOrDefault(u => u.Name == userName);

            model.UserId = user.Id;

            await this.dbContext.AddAsync(this.mapper.Map<Order>(model));

            await this.dbContext.SaveChangesAsync();
        }
    }
}
