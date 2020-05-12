using System;
using AutoMapper;
using ES.API.Filters;
using ES.DataImport.StockExchangeGateways;
using ES.Domain;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using ES.Infrastructure.Mapper;
using ES.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ES.Api
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
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });

            services.Configure<StockExchangeKeys>(Configuration.GetSection(nameof(StockExchangeKeys)));

            services.AddAutoMapper(new Type[]
            {
                typeof(GatewayToDTO),
            });

            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exchange Storage System API", Version = "v1" });
                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\ES.API.XML");
            });

            services.AddDbContext<CoreDBContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString(ContextContstants.ConnectionStringCoreDB));

                options.UseNpgsql(
                    Configuration.GetConnectionString(ContextContstants.ConnectionStringLogsDB));
            });

            services.AddTransient<CryptoCompareGateway>();
            services.AddTransient<ImportMetaDataService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Exchange Storage System API";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exchange Storage System API");
                c.RoutePrefix = "";
            });

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
