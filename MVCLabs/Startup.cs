using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCLabs.Startup))]
namespace MVCLabs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
