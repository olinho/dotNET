﻿using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.LogServices;
using WebApplication1.ObjectsManager.Interfaces;
using WebApplication1.ObjectsManager.LiteDB;
using WebApplication1.ObjectsManager.PostgreSQL;

namespace WebApplication1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterType<ArtistsRepositoryNoSql>().As<IArtistsRepository>().InstancePerRequest();
            builder.RegisterType<PaintingsRepositoryPsql>().As<IPaintingsRepository>().InstancePerRequest();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
