using Microsoft.AspNetCore.Authorization;
using Taste.Utility;

namespace Taste.Pages.Admin.Category
{
    [Authorize(Roles = SD.ManagerRole)]
    public class IndexModel : ApplicationPageModel
    {
        public void OnGet()
        {
        }
    }
}