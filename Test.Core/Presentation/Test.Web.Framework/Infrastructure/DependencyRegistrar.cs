using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Test.Core.Common;
using Test.Core.Configuration;
using Test.Core.Data;
using Test.Core.Infrastructure.DependencyManagement;
using Test.Core.Infrastructure.Engine;
using Test.Core.Infrastructure.TypeFinder;
using Test.Services;
using Test.Data;

namespace Test.Web.Framework.Infrastructure
{
    public class DependencyRegistrar:IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, TestConfig config)
        {

            //help
            builder.RegisterType<CommonHelper>().SingleInstance();

            //services
            builder.RegisterType<StudentService>().InstancePerLifetimeScope();

            //data
            builder.RegisterType<SelfDbContext>().As<DbContext>().InstancePerLifetimeScope();

            //repositories
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }

        public int Order { get; }
    }
}
