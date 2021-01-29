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

    public class IndexModel : ApplicationPageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public IndexModel(IConfiguration configuration, IUnitOfWork unitOfWork, IDbConnectionProvider dbConnectionProvider) : base("Home")
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _dbConnectionProvider = dbConnectionProvider;
        }

        public IEnumerable<MenuItem> MenuItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                int shoppingCartCount = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.ShoppingCart, shoppingCartCount);
            }

            // string connectionString = _configuration.GetConnectionString("DefaultConnection");
            // using (var connection = new MySqlConnection(connectionString))
            // {
            //     MenuItemList = connection.Query<MenuItem>(getMenuQuery);
            //     CategoryList = connection.Query<Category>(getCategoryQuery);
            // }
            using (var connection = _dbConnectionProvider.GetRemoteDbConnection())
            {
                connection.Open();

                string listMenuItemQuery = _dbConnectionProvider.Queries["ListMenuItem"];
                MenuItemList = connection.Query<MenuItem>(listMenuItemQuery);

                string listCategoryQuery = _dbConnectionProvider.Queries["ListCategory"];
                CategoryList = connection.Query<Category>(listCategoryQuery);
            }
        }
    }
}