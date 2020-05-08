using System;
using AutoMapper;
using ES.Domain;
using ES.Domain.Configurations;
using ES.Domain.Constants;
using ES.Infrastructure.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddControllers();

            services.Configure<StockExchangeKeys>(Configuration.GetSection(nameof(StockExchangeKeys)));

            services.AddAutoMapper(new Type[]
            {
                typeof(GatewayToDTO),
            });


            services.AddDbContext<CoreDBContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString(ContextContstants.ConnectionStringName));
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
        }
    }
}
