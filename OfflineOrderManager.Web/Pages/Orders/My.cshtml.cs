using Microsoft.AspNetCore.Mvc;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Models.View.Orders;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;
using System.Linq;

namespace OfflineOrderManager.Web.Pages.Orders
{
    [BindProperties]
    public class MyModel : ListingOrderModel
    {
        public MyModel(IEntityService entityService, IMappingService mapper)
            : base(entityService, mapper) { }

        public override void OnGet()
        {
            var orders = entityService.GetAll<Order>(o => o.Author == this.User.Identity.Name);

            this.Orders = orders.Select(this.mapper.Map<OrderViewModel>).ToList();
        }
    }
}