using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entitys;

namespace Ordering.Repository
{

    public class OrderRepository : IOrderRepository<Order> 
    {
        private readonly IMediator mediator;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(IMediator mediator, ILogger<OrderRepository> logger)
        {
            this.mediator = mediator;
            this._logger = logger;
        }

        public async Task SaveAsAsync(Order entity)
        {
            this._logger.LogWarning("Dispatched Save Repository");
            await this.mediator.DispatchDomainEventsAsync(new List<Order>() { entity });
        }
    }
}
