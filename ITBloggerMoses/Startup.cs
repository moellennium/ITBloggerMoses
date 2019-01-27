using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITBloggerMoses.Startup))]
namespace ITBloggerMoses
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
