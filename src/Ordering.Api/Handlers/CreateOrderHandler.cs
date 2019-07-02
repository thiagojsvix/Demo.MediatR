﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ordering.Api.Command;
using Ordering.Api.Events;
using Ordering.Domain.Core;
using Ordering.Domain.Entitys;

namespace Ordering.Api.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Response>
    {
        private readonly IMediator mediator;

        public CreateOrderHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Response> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            var order = new Order(request.CustomerId, request.Discount, request.Paid);
            
            var orderEvent = new CreateOrderEvent(request);

            await this.mediator.Publish(orderEvent);

            return new Response($"Custmer Id: {order.CustomerId}");
        }
    }
}