using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AutoqueryManyToManyFilter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var aspnetEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            NLog.Logger logger = null;
            if (aspnetEnvironment == "Development")
            {
                logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.dev.config").GetCurrentClassLogger();
            }
            else
            {
                logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            }
            try
            {
                logger.Info("init main");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
