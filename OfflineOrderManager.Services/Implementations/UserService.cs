using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public async Task Register(UserServiceModel model)
        {
            model.Password = this.ComputeSha256Hash(model.Password);

            var user = this.mapper.Map<User>(model);

            await dbContext.AddAsync(user);

            await dbContext.SaveChangesAsync();
        }

        public bool Exists(string userName) =>
            this.dbContext
            .Users
            .Any(u => u.Name == userName);

        public bool Exists(UserServiceModel model) => 
            this.dbContext
            .Users
            .FirstOrDefault(u => u.Name == model.Name && u.Password == this.ComputeSha256Hash(model.Password)) != null;

        private string ComputeSha256Hash(string password)
        {
            byte[] bytes = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
