using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Taste.Pages.Details
{
    public class IndexModel : ApplicationPageModel
    {
        public IndexModel() : base("Product details")
        {
        }

        public void OnGet()
        {
        }
    }
}