
using System;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Api.Behaviors;

namespace Ordering.Api.Extension
{
    public static class MediatRHandlerExtension
    {
        public static IServiceCollection AddMediatRHandler(this IServiceCollection services)
        {
            const string applicationAssemblyName = "Ordering.Api";
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
            services.AddMediatR(assembly);

            return services;
        }
    }
}
