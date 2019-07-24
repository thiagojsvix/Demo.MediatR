using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Api.EventHandlers
{
    public class OrderMailEventHandler : INotificationHandler<CreateOrderEvent>
    {
        private readonly ILogger<OrderMailEventHandler> logger;
        public OrderMailEventHandler(ILogger<OrderMailEventHandler> logger)
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