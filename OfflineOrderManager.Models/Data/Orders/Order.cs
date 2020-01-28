using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Models.Services.Orders;
using OfflineOrderManager.Utils.AutoMapper.Contracts;
using System;

namespace OfflineOrderManager.Models.Data.Orders
{
    public class Order : IMapFrom<OrderServiceModel>
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Amount { get; set; }

        public string Payed { get; set; }

        public string LeftToPay { get; set; }

        public string Comment { get; set; }

        public string CustomerName { get; set; }

        public string CustormerPhoneNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public int Status { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
