using System.Collections.Generic;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Models.Services.Registration;
using OfflineOrderManager.Utils.AutoMapper.Contracts;

namespace OfflineOrderManager.Models.Data.Users
{
    public class User : IMapFrom<UserServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
