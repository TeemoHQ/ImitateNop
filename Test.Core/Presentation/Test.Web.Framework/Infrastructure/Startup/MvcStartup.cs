using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Core.Infrastructure.Engine;
using Test.Core.Infrastructure.Startup;

namespace Test.Web.Framework.Infrastructure.Startup
{
    public class MvcStartup: ISelfStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            //Do somthing
        }

        public void Configure(IApplicationBuilder app)
        {
            var env = EngineContext.Current.Resolve<IHostingEnvironment>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public int Order { get; } = 1;
    }
}
