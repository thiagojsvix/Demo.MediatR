using FluentValidation;
using Ordering.Domain.Entitys;

namespace Ordering.Domain.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            this.RuleFor(x => x.CustomerId).NotEmpty();
            this.RuleFor(x => x.OrderTotal).GreaterThanOrEqualTo(0);
        }
    }

    public class AddItemValidator : AbstractValidator<Order>
    {
        public AddItemValidator()
        {
            this.Include(new OrderValidator());
            
            this.RuleFor(x => x.Paid).OrderShouldNotBePaid();
            this.RuleFor(x => x.Discount).Must(this.GrossTotalExceded).WithMessage("Discount cannot exceed the order total").WithErrorCode("GrossTotalExceded");
        }

        private bool GrossTotalExceded(Order order, decimal grossDiscount) => grossDiscount > order.GrossTotal;
    }
}
