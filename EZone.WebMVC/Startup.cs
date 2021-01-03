using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EZone.WebMVC.Startup))]
namespace EZone.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
