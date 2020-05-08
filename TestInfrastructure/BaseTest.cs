using System;
using System.IO;
using AutoMapper;
using ES.Domain;
using ES.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestInfrastructure
{
    public class BaseTest : IDisposable
    {
        protected IConfigurationRoot _configurationRoot;

        protected IMapper _mapper;

        protected DbContextOptions<CoreDBContext> _options;

        private CoreDBContext _context;

        public BaseTest()
        {
            _configurationRoot = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(new Type[]
                {
                    typeof(GatewayToDTO),
                });
            });
            _mapper = mappingConfig.CreateMapper();

            _options = new DbContextOptionsBuilder<CoreDBContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
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
