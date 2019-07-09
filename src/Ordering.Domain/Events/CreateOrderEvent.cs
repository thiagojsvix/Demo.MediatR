using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

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

    public class OrderSaveDb : INotificationHandler<CreateOrderEvent>
    {
        private readonly ILogger<OrderSaveDb> _logger;

        public OrderSaveDb(ILogger<OrderSaveDb> logger)
        {
            this._logger = logger;
        }

        public async Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);
            this._logger.LogWarning("Dispatched Order Save DB");
        }
    }

    public class OrderMail : INotificationHandler<CreateOrderEvent>
    {
        private readonly ILogger<OrderMail> logger;
        public OrderMail(ILogger<OrderMail> logger)
        {
            this.logger = logger;
        }
        public async Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);
            this.logger.LogWarning("Dispatched Order Mail");
        }
    }
}
