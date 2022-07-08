using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(faktura.Web.Startup))]
namespace faktura.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
