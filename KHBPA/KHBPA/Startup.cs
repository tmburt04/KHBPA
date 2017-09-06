using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KHBPA.Startup))]
namespace KHBPA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
