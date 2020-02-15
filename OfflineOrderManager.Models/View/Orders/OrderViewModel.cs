using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Models.Enums;
using OfflineOrderManager.Utils.AutoMapper.Contracts;
using System;

namespace OfflineOrderManager.Models.View.Orders
{
    public class OrderViewModel : IMapFrom<Order>
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

        public Status Status { get; set; }

        public string Author { get; set; }

        public int UserId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
