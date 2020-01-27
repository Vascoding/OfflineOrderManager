using OfflineOrderManager.Models.Services.Orders;
using System.Threading.Tasks;

namespace OfflineOrderManager.Services.Contracts
{
    public interface IOrderService
    {
        Task Add(string userName, OrderServiceModel model);
    }
}