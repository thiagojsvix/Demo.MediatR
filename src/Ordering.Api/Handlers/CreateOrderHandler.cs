
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
        private readonly IOrderRepository<Order> _repository;
        private readonly NotificationList _notificationList;

        public CreateOrderHandler(IOrderRepository<Order> repository, NotificationList _notificationList)
        {
            this._repository = repository;
            this._notificationList = _notificationList;
        }

        public async Task<NotificationList> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);
            var order = new Order(request.CustomerId, request.Discount, request.Paid);

            if (order.Invalid)
            {
                this._notificationList.AddNotification(order.ValidationResult); 
                return this._notificationList;
            }

            await this._repository.SaveAsAsync(order);
            return new NotificationList($"Custmer Id:");
        }
    }
}
