using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;

namespace OfflineOrderManager.Web.Pages.Orders
{
    public class DeleteModel : OrderModel
    {
        public DeleteModel(IEntityService entityService)
            : base(entityService) { }

        public void OnGet(int id)
        {
            var order = this.entityService.GetBy<Order>(o => o.Id == id);

            this.Id = order.Id;
            this.ProductName = order.ProductName;
            this.Amount = order.Amount;
            this.Payed = order.Payed;
            this.LeftToPay = order.LeftToPay;
            this.Comment = order.Comment;
            this.CustomerName = order.CustomerName;
            this.CustormerPhoneNumber = order.CustormerPhoneNumber;
            this.Status = order.Status;
        }

        public IActionResult OnPost()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You need to login first";

                return RedirectToPage(new { id = this.Id });
            }

            this.entityService.DeleteBy<Order>(o => o.Id == this.Id);

            return RedirectToPage("All");
        }
    }
}