using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FlowerStoreAPI.Data;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FlowerStoreAPI
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

            services.AddHttpClient<IBasisRegisterService, BasisRegisterService>(
                // since provider is not used we can discard it (i.e. replace it with an underscore)
                // (provider, client) =>

                (_, client) =>
                {
                    // needless to say, better in config. We pass the api baseuri here.
                    client.BaseAddress = new Uri("https://api.basisregisters.vlaanderen.be");
                });

            //configure connection with MySql database
            services.AddDbContext<FlowerContext>(opt => opt.UseMySql(Configuration.GetConnectionString("SaleCollectionName")));

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IFlowerRepo, SqlFlowerRepo>();
            services.AddScoped<IStoreRepo, SqlStoreRepo>();
            services.AddScoped<ISaleRepo, SqlSaleRepo>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Flowerstore API",
                });

                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                c.IncludeXmlComments(filePath);
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Add a UI for swaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Flowerstore API V1");
            });}
        }
    }

