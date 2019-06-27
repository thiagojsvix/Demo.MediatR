using System;
using FluentValidation;
using MediatR;
using Ordering.Domain.Core;

namespace Ordering.Domain.Command
{
    public class CreateOrderCommand : IRequest<Response>
    {
        public CreateOrderCommand(long customerId, decimal discount, bool paid)
        {
            this.CustomerId = customerId;
            this.Discount = discount;
            this.Paid = paid;
            this.CreationDate = DateTime.UtcNow;
        }

        public long CustomerId { get; private set; }
        public decimal Discount { get; private set; }
        public bool Paid { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? PaymentDate { get; private set; }
    }

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            this.RuleFor(x => x.CustomerId).Must(this.CustormerShouldNotBeEmpty).WithMessage("Customer should not be empty!");
        }

        private bool CustormerShouldNotBeEmpty(long value)
        {
            return value != 0;
        }
    }
}
