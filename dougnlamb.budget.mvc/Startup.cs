using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dougnlamb.budget.mvc.Startup))]
namespace dougnlamb.budget.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
