using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace _1.Apresentacao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               // .ConfigureAppConfiguration((hostingContext, config) =>
               // {
                     
                    //var settings = config.Build();
                   // Serilog.Log.Logger = new LoggerConfiguration()
                        // .MinimumLevel.Debug()
                        // .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                        // .MinimumLevel.Override("System", LogEventLevel.Error)
                         //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                        // .CreateLogger();
                //})
               // .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
