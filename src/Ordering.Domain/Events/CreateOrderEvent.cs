using System;
using MediatR;

namespace Ordering.Domain.Events
{
    public class CreateOrderEvent : INotification
    {
        public CreateOrderEvent(long customerId, decimal discount, bool paid)
        {
            this.CustomerId = customerId;
            this.Discount = discount;
            this.Paid = paid;
            this.CreationDate = DateTime.Now;
            this.PaymentDate = this.Paid ? DateTime.Now : (DateTime?)null;
        }

        public long CustomerId { get; }
        public decimal Discount { get; }
        public bool Paid { get; }
        public DateTime CreationDate { get; }
        public DateTime? PaymentDate { get; }
    }
}
