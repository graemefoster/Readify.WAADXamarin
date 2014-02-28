using Microsoft.Owin.Security.WindowsAzure;
using Owin;

namespace Timesheet.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWindowsAzureBearerToken(new WindowsAzureJwtBearerAuthenticationOptions
                {
                    Tenant = "<directory tenant name>",
                    Audience = "<api / resource uri>"
                });
        }
    }
}