using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Services.Registration;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Extensions;

namespace OfflineOrderManager.Web.Pages
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly IUserService userService;

        public RegisterModel(IUserService userService)
        {
            this.userService = userService;
        }

        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Password { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            var user = new UserServiceModel
            {
                Name = this.Name,
                Password = this.Password
            };

            if (this.userService.Exists(this.Name))
            {
                TempData["ErrorMessage"] = "User allready exists";

                return RedirectToPage();
            }

            await this.userService.Register(user);

            this.SignInAsync(user.Name);

            return RedirectToPage("Index");
        }
    }
}