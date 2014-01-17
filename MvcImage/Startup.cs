using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcImage.Startup))]
namespace MvcImage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
