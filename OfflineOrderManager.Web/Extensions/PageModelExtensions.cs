using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OfflineOrderManager.Web.Extensions
{
    public static class PageModelExtensions
    {
        public static async void SignInAsync(this PageModel pageModel, string name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await pageModel.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
