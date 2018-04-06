using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject.Web.Mvc;

namespace SafeMooney.Server.Infrastructure.Dependencies
{
    public class NinjectWrapper : IDependencyResolver
    {
        protected NinjectDependencyResolver _ninject;

        public NinjectWrapper(NinjectDependencyResolver ninject)
        {
            if (ninject == null)
                throw new ArgumentNullException("ninject is NULL");

            _ninject = ninject;
        }
        public object GetService(Type serviceType)
        {
            return _ninject.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _ninject.GetServices(serviceType);
        }
        public IDependencyScope BeginScope()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}