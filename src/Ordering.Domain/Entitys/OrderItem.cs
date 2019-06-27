using System;
using Ordering.Domain.Core;

namespace Ordering.Domain.Entitys
{
    public class OrderItem: Entity
    {
        private OrderItem() { }

        public static OrderItem New(string productId, int quantity, decimal price, decimal discount = 0)
        {
            if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentNullException(nameof(productId));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));
            if (discount <= 0) throw new ArgumentOutOfRangeException(nameof(discount));
            if (discount > price * quantity) throw new ArgumentException("The discount cannot be more than the total of the order item.", nameof(discount));

            return new OrderItem
            {
                ProductId = productId,
                Quantity = quantity,
                Price = price,
                Discount = discount
            };
        }

        public string ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal Discount { get; private set; }
    }
}
