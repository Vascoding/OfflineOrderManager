using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OfflineOrderManager.Web.Pages.Orders
{
    [BindProperties]
    public class MyModel : PageModel
    {
        public int Number { get; set; }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            var num = this.Number;
        }
    }
}