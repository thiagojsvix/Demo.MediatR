using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Ordering.Domain.Core
{
    public class NotificationList
    {
        private readonly List<Notification> notifications;
        public IReadOnlyCollection<Notification> Notifications => this.notifications;
        public object Result { get; }

        public bool HasNotifications => this.notifications.Any();

        public NotificationList()
        {
            this.notifications = new List<Notification>();
        }

        public NotificationList(object result): this()
        {
            this.Result = result;
        }

        public void AddNotification(string key, string message) =>
            this.notifications.Add(new Notification(key, message));

        public void AddNotification(ValidationFailure validationFailure) =>
            this.AddNotification(validationFailure.ErrorCode, validationFailure.ErrorMessage);

        public void AddNotification(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                this.AddNotification(error.ErrorCode, error.ErrorMessage);
        }

        public void AddNotifications(IEnumerable<ValidationFailure> validationFailures)
        {
            foreach (var validationFailure in validationFailures)
                this.AddNotification(validationFailure);
        }
    }
}
