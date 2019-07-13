using System.Linq;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities.Products;

namespace WebStore.Services.Map
{
    public static class OrderOrderDTO
    {
        public static OrderDTO CopyTo(this Order order, OrderDTO dto)
        {
            if (order is null) return dto;
            dto.Id = order.Id;
            dto.Name = order.Name;
            dto.Address = order.Address;
            dto.Date = order.Date;
            dto.OrderItem = order.OrderItems.Select(item => OrderItemOrderItemDTO.ToDTO(item)).ToArray();
            return dto;
        }

        public static Order CopyTo(this OrderDTO dto, Order order)
        {
            if (dto is null) return order;
            order.Id = dto.Id;
            order.Name = dto.Name;
            order.Address = dto.Address;
            order.Date = dto.Date;
            order.OrderItems = dto.OrderItem.Select(item => item.ToItem()).ToArray();
            return order;
        }

        public static OrderDTO ToDTO(this Order item) => item?.CopyTo(new OrderDTO());

        public static Order ToOrder(this OrderDTO dto) => dto?.CopyTo(new Order());
    }
}