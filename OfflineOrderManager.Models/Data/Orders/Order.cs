using OfflineOrderManager.Models.Data.Users;

namespace OfflineOrderManager.Models.Data.Orders
{
    public class Order
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
