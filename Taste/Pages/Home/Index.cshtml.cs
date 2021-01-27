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

    public class IndexModel : ApplicationPageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IConfiguration configuration, IUnitOfWork unitOfWork) : base("Home")
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
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

            string getMenuQuery = System.IO.File.ReadAllText("Pages/Home/ListMenuItems.sql");
            string getCategoryQuery = System.IO.File.ReadAllText("Pages/Home/ListCategories.sql");
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new MySqlConnection(connectionString))
            {
                MenuItemList = connection.Query<MenuItem>(getMenuQuery);
                CategoryList = connection.Query<Category>(getCategoryQuery);
            }

            //MenuItemList = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType");
            //CategoryList = _unitOfWork.Category.GetAll(null, q => q.OrderBy(c => c.DisplayOrder), null);
        }
    }
}