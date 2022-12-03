using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Monitoring.Sales.API.Infrastructure.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace Microservices.Monitoring.Sales.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
                //.WriteTo.File("d:\\Logs\\Microservices.Monitoring.Sales.API.txt")
              //  .CreateLogger();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Http("http://localhost:28080")
                .Enrich.With(new CustomLogEnricher())
                .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) => {

                    var hostingEnvironment = webHostBuilderContext.HostingEnvironment;
                    configurationBuilder.AddConfigServer(hostingEnvironment.EnvironmentName);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
