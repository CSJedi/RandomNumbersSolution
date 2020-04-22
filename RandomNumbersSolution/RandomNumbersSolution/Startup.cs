using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RandomNumbersSolution.Startup))]
namespace RandomNumbersSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
