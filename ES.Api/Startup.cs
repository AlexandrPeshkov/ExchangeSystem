using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using ES.API.Filters;
using ES.DataImport.StockExchangeGateways;
using ES.DataImport.UseCase;
using ES.Domain;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using ES.Domain.Interfaces.UseCases;
using ES.Infrastructure.Mapper;
using ES.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

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
            });

            services.AddTransient<CryptoCompareGateway>();
            services.AddTransient<ImportColdDataService>();

            //services.AddSingleton<ExchangeHolder>();
            //services.AddSingleton<CurrencyHolder>();
            AddUseCases(services);
        }

        private void AddUseCases(IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(BaseGatewayUseCase<,,>))
                .GetTypes()
                .Where(t => t.IsGenericType == false && t.GetInterfaces().FirstOrDefault(i => i?.Name == typeof(IUseCase<,,>)?.Name) != null);

            foreach (var useCase in assembly)
            {
                services.AddTransient(useCase);
            }
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
            app.UseSerilogRequestLogging();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
