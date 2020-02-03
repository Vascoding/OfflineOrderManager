using Microsoft.AspNetCore.Mvc;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;
using System;
using System.Threading.Tasks;

namespace OfflineOrderManager.Web.Pages.Orders
{
    public class EditModel : OrderModel
    {
        public EditModel(IEntityService entityService) 
            : base(entityService) { }

        public int Id { get; set; }

        public void OnGet(int id)
        {
            var order = this.entityService.GetBy<Order>(o => o.Id == id);

            this.ProductName = order.ProductName;
            this.Amount = order.Amount;
            this.Payed = order.Payed;
            this.LeftToPay = order.LeftToPay;
            this.Comment = order.Comment;
            this.CustomerName = order.CustomerName;
            this.CustormerPhoneNumber = order.CustormerPhoneNumber;
            this.Status = order.Status;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You need to login first";

                return RedirectToPage();
            }

            var user = this.entityService.GetBy<User>(u => u.Name == this.User.Identity.Name);

            var order = this.entityService.GetBy<Order>(o => o.Id == this.Id);


            order.ProductName = this.ProductName;
            order.Amount = this.Amount;
            order.Payed = this.Payed;
            order.LeftToPay = this.Amount - this.Payed;
            order.CustomerName = this.CustomerName;
            order.CustormerPhoneNumber = this.CustormerPhoneNumber;
            order.Comment = this.Comment;
            order.UserId = user.Id;
            order.CreationDate = DateTime.Now;
            order.Status = this.Status;
            order.Author = user.Name;
            

            await this.entityService.AddOrUpdate(order);

            return RedirectToPage("All");
        }
    }
}