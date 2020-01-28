using System;

namespace OfflineOrderManager.Models.Services.Orders
{
    public class OrderServiceModel
    {
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
    }
}
