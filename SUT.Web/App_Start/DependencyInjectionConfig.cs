using Autofac;
using Autofac.Integration.Mvc;
using Sut.Context;
using Sut.IRepositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Sut.Web.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            string nameOrConnectionString = "name=SUTEntities";
            builder.RegisterType<SutContext>().As<DbContext>().WithParameter("nameOrConnectionString", nameOrConnectionString).InstancePerLifetimeScope();
            builder.RegisterType<Repositories.Context>().As<IContext>();
            builder.RegisterType<Repositories.Context>().As<IUnitOfWork>();

            builder.RegisterAssemblyTypes(Assembly.Load("Sut.Repositories"))
                .Where(type => type.Name.EndsWith("Repository", StringComparison.Ordinal))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load("Sut.DomainServices"))
                .Where(type => type.Name.EndsWith("DomainService", StringComparison.Ordinal))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load("Sut.ApplicationServices"))
                .Where(type => type.Name.EndsWith("Service", StringComparison.Ordinal))
                .AsImplementedInterfaces();

            //builder.RegisterType<CauClient>().As<ICauClient>();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}