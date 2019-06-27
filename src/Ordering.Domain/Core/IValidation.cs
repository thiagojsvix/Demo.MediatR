
using FluentValidation;
using FluentValidation.Results;

namespace Ordering.Domain.Core
{
    public interface IValidation
    {
        bool Valid { get; }
        bool Invalid { get; }
        ValidationResult ValidationResult { get; }
        bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator);
    }
}
