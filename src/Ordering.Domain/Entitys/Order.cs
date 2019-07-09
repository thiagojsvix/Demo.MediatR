
using System;
using System.Collections.Generic;
using System.Linq;
using Ordering.Domain.Core;
using Ordering.Domain.Events;
using Ordering.Domain.Validators;

namespace Ordering.Domain.Entitys
{
    public class Order : Entity
    {
        private readonly List<OrderItem> _items;

        public Order(long customerId, decimal discount, bool paid)
        {
            this.CustomerId = customerId;
            this.Discount = discount;
            this.Paid = paid;
            this.CreationDate = DateTime.UtcNow;

            this._items = new List<OrderItem>();

            this.Validate(this, new OrderValidator());
        }

        public long CustomerId { get; private set; }
        public decimal Discount { get; private set; }
        public bool Paid { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? PaymentDate { get; private set; }

        public decimal TotalDiscountOnItems => this.Items.Sum(x => x.Discount);
        public decimal GrossTotal => this.Items.Sum(x => x.Quantity * x.Price);
        public decimal OrderTotal => this.GrossTotal - this.TotalDiscountOnItems - this.Discount;

        public IReadOnlyCollection<OrderItem> Items => this._items.AsReadOnly();

        public void AddItem(OrderItem item)
        {
            _items.Add(item);
            this.Validate(this, new AddItemValidator());
        }

        public void AddOrderDiscount(decimal discount)
        {
            this.Discount = discount;
            this.Validate(this, new AddItemValidator());
        }

        public void PayOrder()
        {
            if (this.Paid) return;

            this.Paid = true;
            this.PaymentDate = DateTime.UtcNow;
        }

        public void AddOrderEvent()
        {
            var orderEvent = new CreateOrderEvent(this.CustomerId, this.Discount, this.Paid);
            this.AddDomainEvent(orderEvent);
        }
    }
}
