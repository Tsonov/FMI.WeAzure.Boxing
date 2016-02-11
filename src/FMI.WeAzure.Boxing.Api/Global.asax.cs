using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Business.Services;
using FMI.WeAzure.Boxing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace FMI.WeAzure.Boxing.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;

            SetupDI(config);

            config.EnsureInitialized();
        }

        protected void SetupDI(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterAssemblyTypes(typeof(IRequestHandler<,>).Assembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                                   .Select(interfaceType => new TypedService(interfaceType)));

            builder.RegisterType<BoxingDbContext>()
                   .InstancePerRequest()
                   .AsSelf()
                   .WithParameter(new TypedParameter(typeof(string), "name=BoxingEntities"));

            builder.RegisterType<PasswordService>()
                   .As<IPasswordService>();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
