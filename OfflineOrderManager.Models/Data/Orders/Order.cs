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

        public decimal Amount { get; set; }

        public decimal Payed { get; set; }

        public decimal LeftToPay { get; set; }

        public string Comment { get; set; }

        public string CustomerName { get; set; }

        public string CustormerPhoneNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public int Status { get; set; }

        public int UserId { get; set; }
    }
}
