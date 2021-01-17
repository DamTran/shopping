using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Utility;

namespace Taste.Pages.AboutUs
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        }
    }
}