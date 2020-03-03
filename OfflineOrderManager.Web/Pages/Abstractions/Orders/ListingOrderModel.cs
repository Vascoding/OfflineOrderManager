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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public int CurrentPage { get; set; } = 1;

        public int NextPage => 
            this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

        public int TotalPages { get; set; }

        public int PageSize => 3;

        public virtual void OnGet(OrdersFilterModel filter, int currentPage = 1)
        {
            this.CurrentPage = currentPage;

            UsersCache.Items = this.entityService.GetAll<User>().Select(u => u.Name).ToList();

            var filterDelegate = this.CreateFilterDelegate(filter);

            var orders = entityService.GetAll(filterDelegate)
                .OrderByDescending(o => o.Id)
                .Select(this.mapper.Map<OrderViewModel>)
                .ToList();

            this.TotalPages = (int)Math.Ceiling((double)orders.Count() / PageSize);

            OrdersCache.Items = orders;
        }

        public IActionResult OnPost(int id, Dictionary<string, string> routeData)
        {
            var order = this.entityService.GetBy<Order>(o => o.Id == id);

            order.Status = (Status)Enum.Parse(typeof(Status), routeData["Status"]);

            this.entityService.AddOrUpdate(order);

            return RedirectToPage();
        }

        public void OnGetPageChange(int currentPage = 1) => 
            this.CurrentPage = currentPage;

        protected virtual Expression GenerateFilterExpression(OrdersFilterModel filter, ParameterExpression parameter)
        {
            var expression = ExpressionBuilder.BuildDefault();

            if (filter.Status != null)
            {
                var body = ExpressionBuilder.Equal(parameter, "Status", filter.Status);

                expression = ExpressionBuilder.AndAlso(expression, body);
            }

            if (filter.From != null)
            {
                var body = ExpressionBuilder.GreaterThanOrEqual(parameter, "CreationDate", filter.From);

                expression = ExpressionBuilder.AndAlso(expression, body);
            }

            if (filter.To != null)
            {
                var body = ExpressionBuilder.LessThanOrEqual(parameter, "CreationDate", filter.To.Value.AddDays(1));

                expression = ExpressionBuilder.AndAlso(expression, body);
            }

            if (filter.Author != null)
            {
                var body = ExpressionBuilder.Equal(parameter, "Author", filter.Author);

                expression = ExpressionBuilder.AndAlso(expression, body);
            }

            if (filter.ProductName != null)
            {
                var body = ExpressionBuilder.ToLowerEqual(parameter, "ProductName", filter.ProductName);

                expression = ExpressionBuilder.AndAlso(expression, body);
            }

            if (filter.Comment != null)
            {
                var body = ExpressionBuilder.ToLowerEqual(parameter, "Comment", filter.Comment);

                expression = ExpressionBuilder.AndAlso(expression, body);
            }

            if (filter.PhoneNumber != null)
            {
                var body = ExpressionBuilder.ToLowerEqual(parameter, "CustormerPhoneNumber", filter.PhoneNumber);

                expression = ExpressionBuilder.AndAlso(expression, body);
            }

            if (filter.CustomerName != null)
            {
                var body = ExpressionBuilder.ToLowerEqual(parameter, "CustomerName", filter.CustomerName);

                expression = ExpressionBuilder.AndAlso(expression, body);
            }

            return expression;
        }

        private Func<Order, bool> CreateFilterDelegate(OrdersFilterModel filter)
        {
            var parameter = ExpressionBuilder.CreateParameter<Order>();

            var expression = this.GenerateFilterExpression(filter, parameter);

            return ExpressionBuilder.CreateLambda<Func<Order, bool>>(expression, parameter);
        }
    }
}
