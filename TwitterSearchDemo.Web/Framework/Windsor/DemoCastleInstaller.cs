using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TwitterSearchDemo.Framework.Windsor
{
    public abstract class DemoCastleInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(FindControllers().Configure(c => c.LifestylePerWebRequest()))
                //the rest are singleton (only TwitterSearch has state, and we want that)
                .Register(Types.FromThisAssembly()
                               .Where(IsCustomTypeWithInterface)
                               .WithService.FirstInterface());
        }

        #endregion

        private bool IsCustomTypeWithInterface(Type x)
        {
            return x.Namespace != null
                   && !x.IsInterface
                   && !x.IsAbstract
                   && x.Namespace.Contains("TwitterSearchDemo")
                   && x.GetInterfaces().Any();
        }

        private BasedOnDescriptor FindControllers()
        {
            return Types.FromThisAssembly()
                        .Where(x => x.Namespace != null && x.Namespace.Contains("TwitterSearchDemo"))
                        .If(t => t.Name.EndsWith("Controller"));
        }
    }
}