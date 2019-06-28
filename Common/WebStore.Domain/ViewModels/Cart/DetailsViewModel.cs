using WebStore.Domain.ViewModels.Order;

namespace WebStore.Domain.ViewModels.Cart
{
    public class DetailsViewModel
    {
        public CartViewModel CartViewModel { get; set; }

        public OrderViewModel OrderViewModel { get; set; }
    }
}