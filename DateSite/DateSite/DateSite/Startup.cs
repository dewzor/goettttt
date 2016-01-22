using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DateSite.Startup))]
namespace DateSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
