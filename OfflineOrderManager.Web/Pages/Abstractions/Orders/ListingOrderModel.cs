using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Models.Enums;
using OfflineOrderManager.Models.View.Filter;
using OfflineOrderManager.Models.View.Orders;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Caches;
using OfflineOrderManager.Web.Helpers;


namespace OfflineOrderManager.Web.Pages.Abstractions.Orders
{
    [BindProperties]
    public abstract class ListingOrderModel : PageModel
    {
        protected readonly IEntityService entityService;
        protected readonly IMappingService mapper;

        public ListingOrderModel(IEntityService entityService, IMappingService mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public Status Status { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int CurrentPage { get; set; }

        public int NextPage => 
            this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

        public int TotalPages =>
            (int)Math.Ceiling((double)OrdersCache.Items.Count() / this.PageSize);

        public int PageSize => 2;

        public virtual async Task OnGet(OrdersFilterModel filter, int currentPage = 1)
        {
            this.CurrentPage = currentPage;

            var users = await this.entityService.GetAll<User>();

            var filterDelegate = new FilterBuilder(filter).CreateFilterDelegate();

            var orders = await entityService.GetAll(filterDelegate);

            UsersCache.Items = users.Select(u => u.Name).ToList();

            OrdersCache.Items = orders.OrderByDescending(o => o.Id)
                .Select(this.mapper.Map<OrderViewModel>)
                .ToList();
        }

        public async Task<IActionResult> OnPost(int id, Dictionary<string, string> routeData)
        {
            var order = await this.entityService.GetBy<Order>(o => o.Id == id);

            order.Status = (Status)Enum.Parse(typeof(Status), routeData["Status"]);

            await this.entityService.AddOrUpdate(order);

            return RedirectToPage();
        }

        public void OnGetPageChange(int currentPage = 1) =>
            this.CurrentPage = currentPage;
    }
}