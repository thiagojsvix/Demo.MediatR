using FluentValidation;
using Ordering.Domain.Validators.Core;

namespace Ordering.Domain.Validators
{
    public static class ExtensionValidators
    {
        /// <summary>
        /// Order should not be paid
        /// </summary>
        public static IRuleBuilderOptions<T, TProperty> OrderShouldNotBePaid<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new PaidOrderValidator());
        }
    }


}
