﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Core.Common;
using Test.Core.Configuration;
using Test.Core.Infrastructure.Startup;
using Test.Core.Infrastructure.TypeFinder;

namespace Test.Core.Infrastructure.Engine
{
    public class Engine:IEngine
    {
        private IServiceProvider _serviceProvider { get; set; }
        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected IServiceProvider GetServiceProvider()
        {
            var accessor = ServiceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            return context != null ? context.RequestServices : ServiceProvider;
        }

        public void Initialize(IServiceCollection services)
        {
            //Application Initialize
            var provider = services.BuildServiceProvider();
            var hostingEnvironment = provider.GetRequiredService<IHostingEnvironment>();
            CommonHelper.BaseDirectory = hostingEnvironment.ContentRootPath;
            //Plugin Initialize

        }

        public IServiceProvider ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            //find startup configurations provided by other assemblies
            var typeFinder = new WebAppTypeFinder();
            var startupConfigurations = typeFinder.FindClassesOfType<ISelfStartup>();

            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Where(startup => true) //先忽视插件
                .Select(startup => (ISelfStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //configure services
            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);

            var nopConfig = services.BuildServiceProvider().GetService<TestConfig>();
            RegisterDependencies(nopConfig, services, typeFinder);
            return _serviceProvider;
        }

        private void RegisterDependencies(TestConfig nopConfig, IServiceCollection services, WebAppTypeFinder typeFinder)
        {
            var containerBuilder=new ContainerBuilder();
            //register engine
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();

            //register type finder
            containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();


        }

        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        public T Resolve<T>() where T : class
        {
            return (T)GetServiceProvider().GetRequiredService(typeof(T));
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <param name="type">Type of resolved service</param>
        /// <returns>Resolved service</returns>
        public object Resolve(Type type)
        {
            return GetServiceProvider().GetRequiredService(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">Type of resolved services</typeparam>
        /// <returns>Collection of resolved services</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <param name="type">Type of service</param>
        /// <returns>Resolved service</returns>
        public virtual object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                            throw new Exception("Unknown dependency");
                        return service;
                    });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }
            throw new Exception("No constructor was found that had all the dependencies satisfied.", innerException);
        }
    }
}
