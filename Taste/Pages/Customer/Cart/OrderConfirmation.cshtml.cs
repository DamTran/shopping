using Microsoft.AspNetCore.Mvc;

namespace Taste.Pages.Customer.Cart
{
    public class OrderConfirmationModel : ApplicationPageModel
    {
        [BindProperty]
        public int orderId { get; set; }

        public void OnGet(int id)
        {
            orderId = id;
        }
    }
}