using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace TwitterSearchDemo.Framework.Windsor
{
    public class WindsorDependencyResolver : WindsorDependencyScope, IDependencyResolver
    {
        private IKernel kernel;
        public WindsorDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(kernel);
        }
    }
}