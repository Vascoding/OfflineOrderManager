using System;

namespace OfflineOrderManager.Models.Services.Orders
{
    public class OrderServiceModel
    {
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
