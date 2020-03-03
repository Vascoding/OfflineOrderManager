using OfflineOrderManager.Models.View.Orders;
using System.Collections.Generic;
using System.Linq;

namespace OfflineOrderManager.Web.Caches
{
    public static class OrdersCache
    {
        public static List<OrderViewModel> Items { get; set; }

        public static List<OrderViewModel> GetAllByPage(int currentPage, int pageSize) =>
            Items
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}
