using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Services.Orders;
using OfflineOrderManager.Services.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
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

        public decimal Amount { get; set; }

        public decimal Payed { get; set; }

        public decimal LeftToPay { get; set; }

        public string Comment { get; set; }

        public string CustomerName { get; set; }

        [Required]
        public string CustormerPhoneNumber { get; set; }

        public int Status { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            var model = new OrderServiceModel
            {
                ProductName = this.ProductName,
                CustomerName = this.CustomerName,
                CustormerPhoneNumber = this.CustormerPhoneNumber,
                Comment = this.Comment
            };

            if (!this.User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You need to login first";

                return RedirectToPage();
            }

            await this.orderService.Add(this.User.Identity.Name, model);

            return RedirectToPage("All");
        }
    }
}