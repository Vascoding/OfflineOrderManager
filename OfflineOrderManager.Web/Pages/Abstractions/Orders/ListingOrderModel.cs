using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Models.Enums;
using OfflineOrderManager.Models.View.Orders;
using OfflineOrderManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OfflineOrderManager.Web.Pages.Abstractions.Orders
{
    [BindProperties]
    public class ListingOrderModel : PageModel
    {
        protected readonly IEntityService entityService;
        protected readonly IMappingService mapper;

        public ListingOrderModel(IEntityService entityService, IMappingService mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }

        public List<OrderViewModel> Orders { get; set; }

        public virtual void OnGet()
        {
            var orders = entityService.GetAll<Order>();

            this.Orders = orders.Select(this.mapper.Map<OrderViewModel>).ToList();
        }

        public IActionResult OnPost(int id, Dictionary<string, string> routeData)
        {
            var order = this.entityService.GetBy<Order>(o => o.Id == id);

            order.Status = (Status)Enum.Parse(typeof(Status), routeData["Status"]);

            this.entityService.AddOrUpdate(order);

            return RedirectToPage();
        }
    }
}
