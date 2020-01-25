using System.Linq;
using System.Threading.Tasks;
using OfflineOrderManager.Data;
using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Models.Services.Registration;
using OfflineOrderManager.Services.Contracts;

namespace OfflineOrderManager.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly OfflineOrderManagerDbContext dbContext;
        private readonly IMappingService mapper;

        public UserService(OfflineOrderManagerDbContext dbContext, IMappingService mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task RegisterUser(UserServiceModel model)
        {
            var user = this.mapper.Map<User>(model);

            await dbContext.AddAsync(user);

            await dbContext.SaveChangesAsync();
        }

        public bool Exists(UserServiceModel model) =>
            this.dbContext
            .Users
            .Any(u => u.Name == model.Name && u.Password == model.Password);
    }
}
