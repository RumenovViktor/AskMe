using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForumSystem.Startup))]
namespace ForumSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
