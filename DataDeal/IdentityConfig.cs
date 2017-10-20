using System;
using DataDeal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using DataDeal.Infrastructure;

namespace DataDeal
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DataDbContext>(DataDbContext.Create);
            app.CreatePerOwinContext<jiangliUserManager>(jiangliUserManager.Create);
            app.CreatePerOwinContext<jiangliRoleManager>(jiangliRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),//请求内容重定向地址
            });
        }
    }
}
