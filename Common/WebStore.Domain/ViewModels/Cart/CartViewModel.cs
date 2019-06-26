using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.ViewModels.Product;

namespace WebStore.Domain.ViewModels.Cart
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; } = new Dictionary<ProductViewModel, int>();

        public int ItemsCount => Items?.Sum(item => item.Value) ?? 0;
    }
}
