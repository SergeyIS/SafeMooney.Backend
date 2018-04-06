using Ninject;
using Ninject.Web.Mvc;
using SafeMooney.Server.Infrastructure.Dependencies;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dependencies;

namespace SafeMooney.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{_username}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //configurate of ninject IoC container
            var kernel = new StandardKernel(new NinjectBindingConfig());
            DependencyContainer.SetResolver(new NinjectWrapper(new NinjectDependencyResolver(kernel)));

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
