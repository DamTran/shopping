namespace Taste.Pages.Home
{
    using Dapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Taste.DataAccess.Data.Repository.IRepository;
    using Taste.Models;
    using Taste.Utility;
    using Taste.DataAccess.DbConnectionProvider;
    using System.Data;
    using Taste.DataAccess;

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
            // TODO: Create logic for getting cart
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            using (var connection = _dbConnectionProvider.GetRemoteDbConnection())
            {
                connection.Open();

                if (claim != null)
                {
                    string getCartItemCountQuery = _dbConnectionProvider.Queries[EnumSqlQuery.GetCartItemCount];
                    int cartItemCount = connection.ExecuteScalar<int>(getCartItemCountQuery, new
                    {
                        ApplicationUserId = claim.Value,
                    });
                    HttpContext.Session.SetInt32(SD.ShoppingCart, cartItemCount);
                }

                string listMenuItemQuery = _dbConnectionProvider.Queries[EnumSqlQuery.ListMenuItem];
                MenuItemList = connection.Query<MenuItem>(listMenuItemQuery);

                string listCategoryQuery = _dbConnectionProvider.Queries[EnumSqlQuery.ListCategory];
                CategoryList = connection.Query<Category>(listCategoryQuery);
            }
        }
    }
}