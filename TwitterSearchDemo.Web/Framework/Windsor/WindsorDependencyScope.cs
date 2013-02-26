using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;

namespace TwitterSearchDemo.Framework.Windsor
{
    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IKernel _kernel;

        public WindsorDependencyScope(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Dispose()
        {

        }

        public object GetService(Type serviceType)
        {
            if (_kernel.HasComponent(serviceType))
                return _kernel.Resolve(serviceType);
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_kernel.HasComponent(serviceType))
                return _kernel.ResolveAll(serviceType).Cast<object>();
            return new List<object>();
        }
    }
}