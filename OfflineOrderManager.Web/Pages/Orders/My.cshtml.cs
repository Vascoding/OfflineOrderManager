using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfflineOrderManager.Models.View.Filter;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Helpers;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;

namespace OfflineOrderManager.Web.Pages.Orders
{
    [BindProperties]
    public class MyModel : ListingOrderModel
    {
        public MyModel(IEntityService entityService, IMappingService mapper)
            : base(entityService, mapper) { }

        public override async Task OnGet(OrdersFilterModel filter, int currentPage = 1)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                filter.Author = User.Identity.Name;
            }

            await base.OnGet(filter, currentPage);
        }
    }
}