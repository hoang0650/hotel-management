using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tikasa.Startup))]
namespace Tikasa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
