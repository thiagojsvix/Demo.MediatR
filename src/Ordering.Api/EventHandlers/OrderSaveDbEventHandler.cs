using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Api.EventHandlers
{
    public class OrderSaveDbEventHandler : INotificationHandler<CreateOrderEvent>
    {
        private readonly ILogger<OrderSaveDbEventHandler> _logger;

        public OrderSaveDbEventHandler(ILogger<OrderSaveDbEventHandler> logger)
        {
            this._logger = logger;
        }

        public async Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);
            this._logger.LogWarning("Dispatched Order Save DB");
        }
    }
}
