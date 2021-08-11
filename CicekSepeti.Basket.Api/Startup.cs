using CicekSepeti.Basket.Core.Validator;
using CicekSepeti.Basket.IOC;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekSepeti.Basket.Core;
using System.Net;
using CicekSepeti.BasketCore;
using CicekSepeti.Basket.Core.Model;

namespace CicekSepeti.BasketApi
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CicekSepeti.BasketApi", Version = "v1" });
            });

            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoDbSettings:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoDbSettings:Database").Value;
            });

            ServiceRegistry.Register(services, Configuration);

            ConfigureFluentValidation(services);

            ConfigureRedis(services);
        }

        private void ConfigureRedis(IServiceCollection services)
        {
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration["RedisCache:ConnectionString"];
            });
        }

        private static void ConfigureFluentValidation(IServiceCollection services)
        {
            services.AddMvcCore()
            .AddFluentValidation(fvc =>
            {
                fvc.RegisterValidatorsFromAssemblyContaining<BasketApiRequestModelValidator>();
                fvc.RegisterValidatorsFromAssemblyContaining<ProductApiRequestModelValidator>();
                fvc.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            })
            .AddApiExplorer()
            .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var validationMessages = context.ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .Select(x => string.Join(",", x.Value.Errors.Select(y => y.ErrorMessage).ToArray()));

                    string message = string.Join(",", validationMessages.ToArray());

                    return new OkObjectResult(
                        new BasketApiResponseModel()
                        {
                            HttpStatusCode = HttpStatusCode.BadRequest.GetHashCode(),
                            ExceptionMessage = message
                        }
                    );
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CicekSepeti.BasketApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
