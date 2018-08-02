using Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pivotal.Discovery.Client;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace Preferences
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .AddCloudFoundry();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<CloudFoundryApplicationOptions>(Configuration);
            services.Configure<CloudFoundryServicesOptions>(Configuration); 
            services.AddDbContext<MessagingContext>();            
            services.AddDiscoveryClient(Configuration);
            services.AddMvc();   
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddConsole();


            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Root}/{action=Get}/{id?}");
            });        
            
            // Use the Steeltoe Discovery Client service
            app.UseDiscoveryClient();              
        }               
    }
}
