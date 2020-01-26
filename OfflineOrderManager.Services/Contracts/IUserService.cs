using System.Threading.Tasks;
using OfflineOrderManager.Models.Services.Registration;

namespace OfflineOrderManager.Services.Contracts
{
    public interface IUserService
    {
        Task Register(UserServiceModel model);
        bool Exists(string userName);
        bool Exists(UserServiceModel model);
    }
}
