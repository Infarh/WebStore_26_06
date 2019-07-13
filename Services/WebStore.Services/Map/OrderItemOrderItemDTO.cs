using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities.Products;

namespace WebStore.Services.Map
{
    public static class OrderItemOrderItemDTO
    {
        public static OrderItemDTO CopyTo(this OrderItem item, OrderItemDTO dto)
        {
            if (item is null) return dto;
            dto.Id = item.Id;
            dto.Quantity = item.Quantity;
            dto.Price = item.Price;
            return dto;
        }

        public static OrderItem CopyTo(this OrderItemDTO dto, OrderItem item)
        {
            if (dto is null) return item;
            item.Id = dto.Id;
            item.Quantity = dto.Quantity;
            item.Price = dto.Price;
            return item;
        }

        public static OrderItemDTO ToDTO(this OrderItem item) => item?.CopyTo(new OrderItemDTO());

        public static OrderItem ToItem(this OrderItemDTO dto) => dto?.CopyTo(new OrderItem());
    }
}
