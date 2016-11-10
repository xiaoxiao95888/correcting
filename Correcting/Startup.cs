using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Correcting.Startup))]
namespace Correcting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
