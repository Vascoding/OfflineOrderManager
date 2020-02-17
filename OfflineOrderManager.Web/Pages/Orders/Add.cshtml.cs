using Microsoft.AspNetCore.Mvc;
using OfflineOrderManager.Services.Contracts;
using System.Threading.Tasks;
using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Models.Data.Orders;
using System;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;
using System.Globalization;

namespace OfflineOrderManager.Web.Pages.Orders
{
    [BindProperties]
    public class AddModel : OrderModel
    {
        public AddModel(IEntityService entityService) 
            : base(entityService) { }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You need to login first";

                return RedirectToPage();
            }

            var user = this.entityService.GetBy<User>(u => u.Name == this.User.Identity.Name);

            decimal.TryParse(this.Amount.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount);
            decimal.TryParse(this.Payed.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal payed);

            var model = new Order
            {
                ProductName = this.ProductName,
                Amount = amount,
                Payed = payed,
                LeftToPay = amount - payed,
                CustomerName = this.CustomerName,
                CustormerPhoneNumber = this.CustormerPhoneNumber,
                Comment = this.Comment,
                UserId = user.Id,
                CreationDate = DateTime.Now,
                Status = this.Status,
                Author = user.Name
            };

            this.entityService.AddOrUpdate(model);

            return RedirectToPage("All");
        }
    }
}