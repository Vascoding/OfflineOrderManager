using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Services.Registration;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Extensions;

namespace OfflineOrderManager.Web.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IUserService userService;

        public LoginModel(IUserService userService)
        {
            this.userService = userService;
        }

        public string Name { get; set; }

        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            var model = new UserServiceModel
            {
                Name = this.Name,
                Password = this.Password
            };

            if (!this.userService.Exists(model))
            {
                TempData["ErrorMessage"] = "Invalid User Credentials";
                return RedirectToPage();
            }

            await this.SignInAsync(model.Name);

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await this.SignOutAsync();

            return RedirectToPage("Index");
        }
    }
}