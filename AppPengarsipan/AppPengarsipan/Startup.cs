using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppPengarsipan.Startup))]
namespace AppPengarsipan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
