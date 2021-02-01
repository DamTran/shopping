namespace Taste.Pages.Home
{
    using Dapper;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Taste.DataAccess.DbConnectionProvider;
    using Taste.Models;
    using Taste.Utility;

    public class IndexModel : ApplicationPageModel
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public IndexModel(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public override string Title => "Home";
        public IEnumerable<MenuItem> MenuItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            using (var connection = _dbConnectionProvider.GetRemoteDbConnection())
            {
                connection.Open();

                if (claim != null)
                {
                    // TODO: Check logic for getting cart item count
                    string getCartItemCountQuery = _dbConnectionProvider.Queries["GetCartItemCount"];
                    //int cartItemCount = connection.ExecuteScalar<int>(getCartItemCountQuery, new
                    //{
                    //    ApplicationUserId = claim.Value,
                    //});
                    //HttpContext.Session.SetInt32(SD.ShoppingCart, cartItemCount);
                    HttpContext.Session.SetInt32(SD.ShoppingCart, 0);
                }

                string listMenuItemQuery = _dbConnectionProvider.Queries["ListMenuItem"];
                MenuItemList = connection.Query<MenuItem>(listMenuItemQuery);

                string listCategoryQuery = _dbConnectionProvider.Queries["ListCategory"];
                CategoryList = connection.Query<Category>(listCategoryQuery);
            }
        }
    }
}