using System.Collections.Generic;
using OfflineOrderManager.Models.Data.Contracts;
using OfflineOrderManager.Models.Data.Orders;

namespace OfflineOrderManager.Models.Data.Users
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
