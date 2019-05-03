using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(GameTech.Startup))]

namespace GameTech
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Home/Login")
            });

            System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier =
            System.Security.Claims.ClaimTypes.Name;
        }
    }
}
