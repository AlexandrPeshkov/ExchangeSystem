using System;
using System.Collections.Generic;
using System.IO;
using ES.Domain.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace ES.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var path = Directory.GetCurrentDirectory();
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
                var configuration = new ConfigurationBuilder()
                   .SetBasePath(path)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables()
                   .Build();

                var connectionString = configuration.GetConnectionString(ContextContstants.ConnectionStringLogsDB);
                var tableName = "Logs";

                IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                {"properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                {"props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
                {"machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
            };

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.PostgreSQL(connectionString, tableName, columnWriters, needAutoCreateTable: true, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                    .WriteTo.Console()
                    .WriteTo.File("Logs/log.log", LogEventLevel.Error)
                    .WriteTo.Seq("http://localhost:5341")
                    //.WriteTo.File($"SerilogLog.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                    .CreateLogger();

                Serilog.Debugging.SelfLog.Enable(OnLoggerError);
                AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
                //Log.Information("Hello, {Name}!", Environment.UserName);
                //Log.CloseAndFlush();
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                var logger = new LoggerConfiguration()
               .MinimumLevel.Error()
               .WriteTo.Console()
               .WriteTo.File("Logs/log.log")
               .CreateLogger();

                logger.Error("Host starting error");
            }
        }

        private static void OnLoggerError(string message)
        {
            var logger = new LoggerConfiguration()
               .MinimumLevel.Error()
               .WriteTo.Console()
               .WriteTo.File("Logs/log.log")
               .CreateLogger();

            logger.Error(message);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog(Log.Logger);
    }
}
