using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(n01625423_Passion_Project.Startup))]
namespace n01625423_Passion_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
