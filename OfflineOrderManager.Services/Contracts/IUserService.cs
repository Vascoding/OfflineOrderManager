using System.Threading.Tasks;
using OfflineOrderManager.Models.Services.Registration;

namespace OfflineOrderManager.Services.Contracts
{
    public interface IUserService
    {
        Task RegisterUser(UserServiceModel model);
        bool Exists(UserServiceModel model);
    }
}
