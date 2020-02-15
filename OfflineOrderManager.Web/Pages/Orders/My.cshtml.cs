using System.Linq.Expressions;
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

        protected override Expression GenerateFilterExpression(OrdersFilterModel filter, ParameterExpression parameter)
        {
            var expression = base.GenerateFilterExpression(filter, parameter);

            var user = this.User.Identity;

            if (user.IsAuthenticated)
            {
                var body = ExpressionBuilder.Equal(parameter, "Author", user.Name);

                expression = ExpressionBuilder.AndAlso(expression, body);
            }

            return expression;
        }
    }
}