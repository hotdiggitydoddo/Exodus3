using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Exodus3.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of unhandled exception");
                throw;
            }


            var webHost = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingCtx, cfg) =>
                {
                    var env = hostingCtx.HostingEnvironment;
                    cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    cfg.AddEnvironmentVariables();

                })
                //.ConfigureLogging((hostingCtx, logging) =>
                //{
                //    logging.AddConfiguration(hostingCtx.Configuration.GetSection("Logging"));
                //    logging.AddConsole();
                //    logging.AddDebug();

                //})
                .UseStartup<Startup>()
                .UseNLog()
                .Build();

            webHost.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
