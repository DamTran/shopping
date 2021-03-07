namespace Taste.Pages.Home
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using Taste.DataAccess.DbConnectionProvider;
    using Taste.Utility;

    public class IndexModel : ApplicationPageModel
    {
        private readonly ITasteDbConnectionProvider _dbConnectionProvider;

        public IndexModel(ITasteDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public override string Title => "Home";

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            using (var connection = _dbConnectionProvider.GetDbConnection())
            {
                connection.Open();

                if (claim != null)
                {
                    HttpContext.Session.SetInt32(SD.ShoppingCart, 0);
                }
            }
        }
    }
}