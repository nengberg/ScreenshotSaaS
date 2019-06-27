using FluentValidation.AspNetCore;

using Hellang.Middleware.ProblemDetails;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Screenshot.API.Features.Batch;
using Screenshot.Domain;
using Screenshot.Infrastructure.MongoDb;
using Screenshot.Infrastructure.RabbitMq;

namespace Screenshot.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining(typeof(SubmitUrlBatchRequestValidator)));

            services.AddProblemDetails();

            services.AddRabbitMq(Configuration);
            services.AddMongoDb(Configuration);
            services.AddTransient<IGetScreenshotsQuery, MongoDbGetScreenshotsQuery>();
        }
        
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
