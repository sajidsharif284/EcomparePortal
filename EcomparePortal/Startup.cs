using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcomparePortal.Startup))]
namespace EcomparePortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
