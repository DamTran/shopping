using Microsoft.AspNetCore.Authorization;
using Taste.Utility;

namespace Taste.Pages.Admin.FoodType
{
    [Authorize(Roles = SD.ManagerRole)]
    public class IndexModel : ApplicationPageModel
    {
        public void OnGet()
        {
        }
    }
}