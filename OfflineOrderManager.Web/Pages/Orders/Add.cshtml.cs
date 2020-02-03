using Microsoft.AspNetCore.Mvc;
using OfflineOrderManager.Services.Contracts;
using System.Threading.Tasks;
using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Models.Data.Orders;
using System;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;

namespace OfflineOrderManager.Web.Pages.Orders
{
    [BindProperties]
    public class AddModel : OrderModel
    {
        public AddModel(IEntityService entityService) 
            : base(entityService) { }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You need to login first";

                return RedirectToPage();
            }

            var user = this.entityService.GetBy<User>(u => u.Name == this.User.Identity.Name);

            var model = new Order
            {
                ProductName = this.ProductName,
                Amount = this.Amount,
                Payed = this.Payed,
                LeftToPay = this.Amount - this.Payed,
                CustomerName = this.CustomerName,
                CustormerPhoneNumber = this.CustormerPhoneNumber,
                Comment = this.Comment,
                UserId = user.Id,
                CreationDate = DateTime.Now,
                Status = this.Status,
                Author = user.Name
            };

            await this.entityService.AddOrUpdate(model);

            return RedirectToPage("All");
        }
    }
}