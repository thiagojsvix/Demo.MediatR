
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ordering.Api.Command;
using Ordering.Domain.Core;
using Ordering.Domain.Entitys;
using Ordering.Repository;

namespace Ordering.Api.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, NotificationList>
    {
        private readonly IMediator mediator;
        private readonly IOrderRepository<Order> _repository;

        public CreateOrderHandler(IMediator mediator, IOrderRepository<Order> repository)
        {
            this.mediator = mediator;
            this._repository = repository;
        }

        public async Task<NotificationList> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);
            var order = new Order(request.CustomerId, request.Discount, request.Paid);
            await this._repository.SaveAsAsync(order);
            return new NotificationList($"Custmer Id:");
        }
    }
}
