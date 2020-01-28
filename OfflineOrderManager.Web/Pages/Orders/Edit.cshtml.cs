using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace OfflineOrderManager.Web.Pages.Orders
{
    public class EditModel : PageModel
    {
        public string ProductName { get; set; }

        public string Amount { get; set; }

        public string Payed { get; set; }

        public string LeftToPay { get; set; }

        public string Comment { get; set; }

        public string CustomerName { get; set; }

        [Required]
        public string CustormerPhoneNumber { get; set; }

        public int Status { get; set; }
        public void OnGet()
        {

        }
    }
}