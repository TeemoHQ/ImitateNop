using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Core.Configuration;
using Test.Core.Infrastructure.Engine;

namespace Test.Web.Framework.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            if (services == null|| configuration == null)
                throw new ArgumentNullException(nameof(services));
            //加载config
            services.ConfigureStartupConfig<TestConfig>(configuration.GetSection("Test"));
            //加载http上下文
            services.AddHttpContextAccessor();
            //加载MVC
            services.AddMvcCore();
            //engine初始化
            var engine = EngineContext.Create();
            engine.Initialize(services);
            //engine处理services并且autofac注入【也是扩展方法处理，最后回到这里】
            var serviceProvider = engine.ConfigureServices(services, configuration);

            return serviceProvider;
        }

        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
