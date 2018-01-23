using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Core.Infrastructure.Engine;
using Test.Core.Infrastructure.Startup;
using Test.Data;

namespace Test.Web.Framework.Infrastructure.Startup
{
    public class DataBaseStartup: ISelfStartup
    {


        public void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<SelfDbContext>(x => x.UseMySql(configuration.GetConnectionString("MySql")));
        }

        public void Configure(IApplicationBuilder application)
        {
            var dbContext=EngineContext.Current.ServiceProvider.GetService<SelfDbContext>();
            dbContext.Database.EnsureCreated();
        }

        public int Order { get; } = 1;
    }
}
