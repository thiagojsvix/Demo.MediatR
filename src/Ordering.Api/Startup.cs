
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Api.Extension;
using Ordering.Api.Filters;
using Ordering.Domain.Core;
using Ordering.Repository;

namespace Ordering.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<NotificationList>();
            services.AddScoped(typeof(IOrderRepository<Domain.Entitys.Order>), typeof(OrderRepository));
            
            services.AddGlobalExceptionHandler()
                    .AddMediatRHandler();

            services.AddMvc(options => options.Filters.Add<NotificationFilter>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseGlobalExceptionHandler();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
