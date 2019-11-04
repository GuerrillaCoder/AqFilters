using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Funq;
using ServiceStack;
using ServiceStack.Configuration;
using AutoqueryManyToManyFilter.ServiceInterface;
using ServiceStack.Script;
using ServiceStack.Web;
using System;
using ServiceStack.Admin;
using ServiceStack.Text;
using ServiceStack.Logging;
using ServiceStack.Data;
using ServiceStack.Logging.NLogger;
using ServiceStack.OrmLite;


namespace AutoqueryManyToManyFilter
{
    public class Startup : ModularStartup
    {
        public Startup(IConfiguration configuration) 
            : base(configuration, typeof(MyServices).Assembly) {}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public new void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("AutoqueryManyToManyFilter", typeof(MyServices).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            //Register logger
            LogManager.LogFactory = new NLogFactory();

            Plugins.Add(new SharpPagesFeature()); // enable server-side rendering, see: https://sharpscript.net/docs/sharp-pages

            SetConfig(new HostConfig
            {
                UseSameSiteCookies = true,
                AddRedirectParamsToQueryString = true,
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), HostingEnvironment.IsDevelopment()),
            });

            var conFactory = new OrmLiteConnectionFactory(AppSettings.GetString("connectionString"), PostgreSqlDialect.Provider);

            container.Register<IDbConnectionFactory>(conFactory);

            Plugins.Add(new AutoQueryFeature
            {
                MaxLimit = 100,
                StripUpperInLike = false,
                EnableAutoQueryViewer = true
            });
            Plugins.Add(new AdminFeature());

            SeedDatabase.Seed();
        }
    }
}
