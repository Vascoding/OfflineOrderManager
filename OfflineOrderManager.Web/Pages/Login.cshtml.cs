﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OfflineOrderManager.Models.Data.Users;
using OfflineOrderManager.Services.Contracts;
using OfflineOrderManager.Web.Extensions;
using OfflineOrderManager.Web.Pages.Abstractions.Account;

namespace OfflineOrderManager.Web.Pages
{
    public class LoginModel : AccountPageModel
    {
        public LoginModel(IEntityService entityService) 
            : base (entityService) { }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            var user = await this.entityService.GetBy<User>(u => u.Name == this.Name && u.Password == this.ComputeSha256Hash(this.Password));

            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid User Credentials";

                return RedirectToPage();
            }

            var model = new User
            {
                Name = this.Name,
                Password = this.Password
            };

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