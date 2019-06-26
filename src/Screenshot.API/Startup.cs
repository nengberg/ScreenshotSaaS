using System;

using FluentValidation.AspNetCore;

using Hellang.Middleware.ProblemDetails;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

using Screenshot.API.Features.Batch;
using Screenshot.Infrastructure;

namespace Screenshot.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining(typeof(SubmitUrlBatchRequestValidator)));

            services.AddProblemDetails();

            services.AddSingleton<IMessageBroker>(
                c =>
                {
                    var connectionFactory = new ConnectionFactory();
                    Configuration.GetSection("RabbitMqConnection").Bind(connectionFactory);
                    return new RabbitMqMessageBroker(connectionFactory, c.GetService<IServiceScopeFactory>());
                });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseProblemDetails();
            app.UseMvc();
        }
    }
}
