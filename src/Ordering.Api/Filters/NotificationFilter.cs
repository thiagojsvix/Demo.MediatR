using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Ordering.Domain.Core;

namespace Ordering.Api.Filters
{
    public class NotificationFilter: IAsyncResultFilter
    {
        private readonly NotificationList _notificationList;

        public NotificationFilter(NotificationList _notificationList)
        {
            this._notificationList = _notificationList;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (this._notificationList.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                context.HttpContext.Response.ContentType = "application/json";

                var notifications = JsonConvert.SerializeObject(this._notificationList.Notifications);
                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}
