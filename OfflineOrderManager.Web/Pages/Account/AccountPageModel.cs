using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfflineOrderManager.Services.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace OfflineOrderManager.Web.Pages.Account
{
    [BindProperties]
    public abstract class AccountPageModel : PageModel
    {
        protected readonly IEntityService entityService;

        public AccountPageModel(IEntityService entityService)
        {
            this.entityService = entityService;
        }

        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Password { get; set; }

        protected string ComputeSha256Hash(string password)
        {
            byte[] bytes = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
