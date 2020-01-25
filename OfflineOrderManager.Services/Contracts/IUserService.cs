using System.Threading.Tasks;
using OfflineOrderManager.Models.Services.Registration;

namespace OfflineOrderManager.Services.Contracts
{
    public interface IUserService
    {
        Task RegisterUser(RegisterServiceModel model);
    }
}
