using System.Collections.Generic;
using System.Linq;

namespace Taste.Pages.Cart
{
    // TODO: https://livedemo00.template-help.com/wt_prod-10492/theme/cart-page.html
    public class IndexModel : ApplicationPageModel
    {
        public override string Title => "Cart";
        public IList<CartItem> Items { get; }

        public IndexModel()
        {
            Items = new List<CartItem>();
        }

        public void OnGet()
        {
        }

        public class CartItem
        {
            public int CartItemId { get; set; }
            public int ProductId { get; set; }
            public string ProductTitle { get; set; }
            public int Quantity { get; set; }
            public int Price { get; set; }
        }
    }
}