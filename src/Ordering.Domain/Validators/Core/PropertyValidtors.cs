using FluentValidation.Validators;

namespace Ordering.Domain.Validators.Core
{
    public class PaidOrderValidator : PropertyValidator
    {
        public PaidOrderValidator() : base("This Order is already paid") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var paid = context.PropertyValue as bool?;
            return paid == true;
        }
    }
}
