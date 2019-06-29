using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.Entities.Products
{
    public class Order : NamedEntity
    {
        public virtual User User { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem : BaseEntity
    {
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
