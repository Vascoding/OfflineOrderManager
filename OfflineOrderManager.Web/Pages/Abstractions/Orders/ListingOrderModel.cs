using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Models.Enums;
using OfflineOrderManager.Models.View.Filter;
using OfflineOrderManager.Models.View.Orders;
using OfflineOrderManager.Services.Contracts;
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

        public List<string> Authors { get; set; }

        public Status Status { get; set; }

        public List<OrderViewModel> Orders { get; set; }

        public virtual void OnGet(OrdersFilterModel filter)
        {
            this.Authors = this.entityService.GetAll<User>().Select(u => u.Name).ToList();

            var filterDelegate = this.CreateFilterDelegate(filter);

            var orders = entityService.GetAll(filterDelegate);

            this.Orders = orders.Select(this.mapper.Map<OrderViewModel>).OrderByDescending(o => o.Id).ToList();
        }

        public IActionResult OnPost(int id, Dictionary<string, string> routeData)
        {
            var order = this.entityService.GetBy<Order>(o => o.Id == id);

            order.Status = (Status)Enum.Parse(typeof(Status), routeData["Status"]);

            this.entityService.AddOrUpdate(order);

            return RedirectToPage();
        }

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
