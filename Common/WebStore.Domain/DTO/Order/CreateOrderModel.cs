using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.ViewModels.Order;

namespace WebStore.Domain.DTO.Order
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
