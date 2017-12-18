using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CPMCloud.Startup))]
namespace CPMCloud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
