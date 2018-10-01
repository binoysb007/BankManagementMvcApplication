using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RetailBankManagement.Startup))]
namespace RetailBankManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
