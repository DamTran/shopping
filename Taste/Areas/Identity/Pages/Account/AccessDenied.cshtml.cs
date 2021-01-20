using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Taste.Areas.Identity.Pages.Account
{
    public class AccessDeniedModel : ApplicationPageModel
    {
        public AccessDeniedModel() : base("Access Denied")
        {
        }

        public void OnGet()
        {
        }
    }
}