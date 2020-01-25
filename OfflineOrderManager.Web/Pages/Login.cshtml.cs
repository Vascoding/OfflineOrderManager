using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Models.Services.Registration;
using OfflineOrderManager.Services.Contracts;

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

        public void OnGet() { }

        public async Task OnPost()
        {
            var model = new UserServiceModel
            {
                Name = this.Name,
                Password = this.Password
            };

            if (!this.userService.Exists(model))
            {
                return;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Name),
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        public async void OnGetLogout() => 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}