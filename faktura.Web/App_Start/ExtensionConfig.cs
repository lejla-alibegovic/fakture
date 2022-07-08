using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Web.Mvc;

namespace faktura.Web.App_Start
{
    public static class ExtensionConfig
    {
        public static void RegisterExtension()
        {
            var container = ConfigureContainer();

            ControllerBuilder.Current.SetControllerFactory(new ExtensionControllerFactory(container));
        }

        private static CompositionContainer ConfigureContainer()
        {
            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var businessRulesCatalog = new AssemblyCatalog(typeof(PDV.IPDVData).Assembly);
            var catalogs = new AggregateCatalog(assemblyCatalog, businessRulesCatalog);
            var container = new CompositionContainer(catalogs);
            return container;
        }
    }
}