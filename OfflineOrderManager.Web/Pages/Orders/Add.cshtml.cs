using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Services.Orders;
using OfflineOrderManager.Services.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Models.Data.Orders;
using System;

namespace OfflineOrderManager.Web.Pages.Orders
{
    [BindProperties]
    public class AddModel : PageModel
    {
        private readonly IEntityService entityService;

        public AddModel(IEntityService entityService)
        {
            this.entityService = entityService;
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
            if (!this.User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You need to login first";

                return RedirectToPage();
            }

            var user = this.entityService.Get<User>(u => u.Name == this.User.Identity.Name);

            var model = new OrderServiceModel
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
                Status = this.Status
            };

            await this.entityService.Add<Order>(model);

            return RedirectToPage("All");
        }
    }
}