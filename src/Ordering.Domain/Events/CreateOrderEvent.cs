
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Command;

namespace Ordering.Domain.Events
{
    public class CreateOrderEvent : INotification
    {
        public CreateOrderEvent(CreateOrderCommand command)
        {
            this.CustomerId = command.CustomerId;
            this.Discount = command.Discount;
            this.Paid = command.Paid;
            this.CreationDate = command.CreationDate;
            this.PaymentDate = command.PaymentDate;
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
            await Task.Delay(0);
            this._logger.LogWarning("Disparado Order Save DB");
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
            await Task.Delay(0);
            this.logger.LogWarning("Disparado Order Mail");
        }
    }
}
