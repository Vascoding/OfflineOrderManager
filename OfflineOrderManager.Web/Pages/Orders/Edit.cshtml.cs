using Microsoft.AspNetCore.Mvc;
using OfflineOrderManager.Models.Data.Orders;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Pages.Abstractions.Orders;
using System.Globalization;
using System.Threading.Tasks;

namespace OfflineOrderManager.Web.Pages.Orders
{
    public class EditModel : OrderModel
    {
        public EditModel(IEntityService entityService) 
            : base(entityService) { }

        public async Task OnGet(int id)
        {
            var order = await this.entityService.GetBy<Order>(o => o.Id == id);

            this.Id = order.Id;
            this.ProductName = order.ProductName;
            this.Amount = order.Amount.ToString(CultureInfo.InvariantCulture);
            this.Payed = order.Payed.ToString(CultureInfo.InvariantCulture);
            this.LeftToPay = order.LeftToPay.ToString(CultureInfo.InvariantCulture);
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

            var order = await this.entityService.GetBy<Order>(o => o.Id == this.Id);

            decimal.TryParse(this.Amount.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount);
            decimal.TryParse(this.Payed.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal payed);

            order.ProductName = this.ProductName;
            order.Amount = amount;
            order.Payed = payed;
            order.LeftToPay = amount - payed;
            order.CustomerName = this.CustomerName;
            order.CustormerPhoneNumber = this.CustormerPhoneNumber;
            order.Comment = this.Comment;
            order.Status = this.Status;
            

            await this.entityService.AddOrUpdate(order);

            return RedirectToPage("All");
        }
    }
}