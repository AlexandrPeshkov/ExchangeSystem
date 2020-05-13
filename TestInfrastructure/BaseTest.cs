using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using ES.DataImport.StockExchangeGateways;
using ES.Domain;
using ES.Domain.Configurations;
using ES.Domain.Interfaces.UseCases;
using ES.Infrastructure.Mapper;
using ES.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestInfrastructure
{
    public class BaseTest : IDisposable
    {
        protected ServiceProvider _services;

        protected DbContextOptions<CoreDBContext> _options;

        private CoreDBContext _context;

        public BaseTest()
        {
            _services = ConfigureServices();
        }

        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var apiPath = $"{AppContext.BaseDirectory}../../../../ES.Api";
            IConfigurationRoot _configurationRoot = new ConfigurationBuilder()
               //.SetBasePath(Directory.GetCurrentDirectory())
               .SetBasePath(apiPath)
               .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            _options = new DbContextOptionsBuilder<CoreDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            var section = _configurationRoot?.GetSection(nameof(StockExchangeKeys));
            services.Configure<StockExchangeKeys>(section);

            services.AddAutoMapper(new Type[]
            {
                typeof(GatewayToDTO),
            });

            services.AddTransient(provider => Context());

            services.AddTransient<CryptoCompareGateway>();
            services.AddTransient<ImportColdDataService>();

            AddUseCases(services);
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private void AddUseCases(IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(IUseCase<,,>))
                .GetTypes()
                .Where(t => t.IsGenericType == false && t.GetInterfaces().FirstOrDefault(i => i?.Name == typeof(IUseCase<,,>)?.Name) != null);

            foreach (var useCase in assembly)
            {
                services.AddTransient(useCase);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        protected CoreDBContext Context()
        {
            _context = new CoreDBContext(_options);
            return _context;
        }
    }
}
