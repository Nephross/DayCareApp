using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DayCareApp.Web.Startup))]
namespace DayCareApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
