using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyFinance.Bizkasa.Startup))]
namespace MyFinance.Bizkasa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
