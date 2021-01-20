using Microsoft.AspNetCore.Mvc;

namespace Taste.Pages.Admin.Order
{
    public class OrderListModel : ApplicationPageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}