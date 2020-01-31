using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Models.Services.Registration;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Extensions;
using OfflineOrderManager.Web.Pages.Account;

namespace OfflineOrderManager.Web.Pages
{
    public class RegisterModel : AccountPageModel
    {
        public RegisterModel(IEntityService entityService)
            : base(entityService) { }

        [StringLength(60, MinimumLength = 3)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            var user = this.entityService.Get<User>(u => u.Name == this.Name);

            if (user != null)
            {
                TempData["ErrorMessage"] = "User allready exists";

                return RedirectToPage();
            }

            var model = new UserServiceModel
            {
                Name = this.Name,
                Password = this.ComputeSha256Hash(this.Password)
            };

            await this.entityService.Add<User>(model);

            await this.SignInAsync(model.Name);

            return RedirectToPage("Index");
        }
    }
}