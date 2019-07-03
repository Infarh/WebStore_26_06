using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.DTO.Order;

namespace WebStore.Domain.ViewModels.Order
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
