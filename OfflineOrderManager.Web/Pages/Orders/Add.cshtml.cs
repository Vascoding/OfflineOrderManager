using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Services.Orders;
using OfflineOrderManager.Services.Contracts;
using System.Threading.Tasks;

namespace OfflineOrderManager.Web.Pages.Orders
{
    [BindProperties]
    public class AddModel : PageModel
    {
        private readonly IOrderService orderService;

        public AddModel(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string CustomerName { get; set; }

        public string CustormerPhoneNumber { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            var model = new OrderServiceModel
            {
                ProductName = this.ProductName,
                CustomerName = this.CustomerName,
                CustormerPhoneNumber = this.CustormerPhoneNumber,
                Description = this.Description
            };

            if (!this.User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You need to login first";

                return RedirectToPage();
            }

            await this.orderService.Add(this.User.Identity.Name, model);

            return RedirectToPage("My");
        }
    }
}