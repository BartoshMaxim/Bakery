using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bakery.Startup))]
namespace Bakery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
