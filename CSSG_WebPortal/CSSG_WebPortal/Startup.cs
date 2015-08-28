using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSSG_WebPortal.Startup))]
namespace CSSG_WebPortal
{
    public partial class Startup {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
