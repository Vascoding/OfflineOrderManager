using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Services.Registration;
using OfflineOrderManager.Services.Contracts;

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

        public string Name { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public void OnGet() { }

        public async Task OnPost()
        {
            await this.userService.RegisterUser(new UserServiceModel
            {
                Name = this.Name,
                Password = this.Password
            });
        }
    }
}