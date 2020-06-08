using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using ES.API.Filters;
using ES.Domain;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using ES.Domain.Interfaces.Gateways;
using ES.Gateway.Interfaces.UseCases;
using ES.Gateway.StockExchangeGateways;
using ES.Gateway.UseCase;
using ES.Infrastructure.Interfaces;
using ES.Infrastructure.Mapper;
using ES.Infrastructure.Services;
using ES.Infrastructure.UseCases;
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
            })
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.Configure<StockExchangeKeys>(Configuration.GetSection(nameof(StockExchangeKeys)));

            services.AddAutoMapper(new Type[]
            {
                typeof(GatewayToDTO),
                typeof(EntityToView),
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exchange Storage System API", Version = "v1" });
                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\ES.API.XML");
            });

            services.AddDbContext<CoreDBContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString(ContextContstants.ConnectionStringCoreDB));
            });

            services.AddTransient<ICryptoCompareGateway, CryptoCompareGateway>();
            services.AddTransient<IAlphaVantageGateway, AlphaVantageGateway>();
            services.AddTransient<ImportColdDataService>();

            services.AddTransient<AccountService>();
            services.AddSingleton<SignalService>();
            services.AddSingleton<EmailService>();

            //services.AddSingleton<ExchangeHolder>();
            //services.AddSingleton<CurrencyHolder>();
            AddGatewayUseCases(services);
            AddContextUseCases(services);
        }

        private void AddGatewayUseCases(IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(BaseGatewayUseCase<,,>))
                .GetTypes()
                .Where(t => t.IsGenericType == false && t.IsAbstract == false && t.GetInterfaces()
                .FirstOrDefault(i => i?.Name == typeof(IGatewayUseCase<,,>)?.Name) != null);

            foreach (var useCase in assembly)
            {
                services.AddTransient(useCase);
            }
        }

        private void AddContextUseCases(IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(BaseContextUseCase<,>))
                .GetTypes()
                .Where(t => t.IsGenericType == false && t.GetInterfaces()
                .FirstOrDefault(i => i?.Name == typeof(IContextUseCase<,>)?.Name) != null);

            foreach (var useCase in assembly)
            {
                services.AddTransient(useCase);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitNonLazy(app);
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

        private void InitNonLazy(IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<SignalService>();
        }
    }
}
