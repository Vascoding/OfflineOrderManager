using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Models.Services.Orders;
using OfflineOrderManager.Utils.AutoMapper.Contracts;

namespace OfflineOrderManager.Models.Data.Orders
{
    public class Order : IMapFrom<OrderServiceModel>
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string CustomerName { get; set; }

        public string CustormerPhoneNumber { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
