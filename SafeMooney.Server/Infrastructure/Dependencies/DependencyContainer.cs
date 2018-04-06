using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace SafeMooney.Server.Infrastructure.Dependencies
{
    public static class DependencyContainer
    {
        private static IDependencyResolver _container;

        public static void SetResolver(IDependencyResolver resolver)
        {
            _container = resolver;
        }

        public static object GetService(Type serviceType)
        {
            try
            {
                return _container.GetService(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.GetServices(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }

    }
}