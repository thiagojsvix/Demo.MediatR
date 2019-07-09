using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Ordering.Domain.Core;

namespace Ordering.Api.Behaviors
{
    public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : NotificationList
    {
        private readonly IEnumerable<IValidator> _validators;
        private readonly NotificationList response;

        public FailFastRequestBehavior(NotificationList response, IEnumerable<IValidator<TRequest>> validators)
        {
            this.response = response;
            this._validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = this._validators
                               .Select(v => v.Validate(request))
                               .SelectMany(result => result.Errors)
                               .Where(f => f != null)
                               .ToList();

            return failures.Any() ? this.Errors(failures) : next();
        }

        private Task<TResponse> Errors(IEnumerable<ValidationFailure> failures)
        {
            this.response.AddNotifications(failures);
            return Task.FromResult(response as TResponse);
        }
    }
}
