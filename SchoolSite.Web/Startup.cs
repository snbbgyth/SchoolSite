using Microsoft.Owin;
using Owin;
using SchoolSite.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace SchoolSite.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
