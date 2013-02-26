using System.Web.Http;
using System.Web.Mvc;
using Castle.Windsor;
using TwitterSearchDemo.Framework.Windsor;

namespace TwitterSearchDemo.App_Start
{
    public class DiConfig : DemoCastleInstaller
    {
        public static void RegisterContainer()
        {
            IWindsorContainer container = new WindsorContainer()
                .Install(new DiConfig());
            var factory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(factory);
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container.Kernel);
        }
    }
}