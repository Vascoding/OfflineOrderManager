using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;
using System.Threading.Tasks;

namespace OfflineOrderManager.Web.Pages.Orders
{
    public class DeleteModel : OrderModel
    {
        public DeleteModel(IEntityService entityService)
            : base(entityService) { }

        public async Task OnGet(int id)
        {
            var order = await this.entityService.GetBy<Order>(o => o.Id == id);

            this.Id = order.Id;
            this.ProductName = order.ProductName;
            this.Amount = order.Amount.ToString();
            this.Payed = order.Payed.ToString();
            this.LeftToPay = order.LeftToPay.ToString();
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

                return RedirectToPage(new { id = this.Id });
            }

            await this.entityService.DeleteBy<Order>(o => o.Id == this.Id);

            return RedirectToPage("All");
        }
    }
}