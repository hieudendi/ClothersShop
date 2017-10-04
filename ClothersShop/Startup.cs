using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClothersShop.Startup))]
namespace ClothersShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
