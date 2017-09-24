using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClintApplication.Startup))]
namespace ClintApplication
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
