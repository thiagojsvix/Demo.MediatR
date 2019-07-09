
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ordering.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                this._logger.LogCritical($"Ocorreu erro {ex}");
                await this.HandlerAsync(context, ex);
            }
        }

        private async Task HandlerAsync(HttpContext context, Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.HttpContext.Request.Path
            };

            if (ex is BadHttpRequestException badHttpRequestException)
            {
                problemDetails.Title = "Requisicao invalida";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Detail = badHttpRequestException.Message;
            }
            else
            {
                problemDetails.Title = ex.Message;
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = ex.ToString();
            }

            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.ContentType = "application/problem+json";

            var json = JsonConvert.SerializeObject(problemDetails, Formatting.Indented);
            await context.Response.WriteAsync(json);
        }
    }
}
