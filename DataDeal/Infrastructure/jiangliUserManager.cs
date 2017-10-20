using System;
using System.Collections.Generic;
using DataDeal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace DataDeal.Infrastructure
{
    public class jiangliUserManager : UserManager<M_JiangliUser>
    {
        public jiangliUserManager(IUserStore<M_JiangliUser> store) : base(store) { }
        public static jiangliUserManager Create(IdentityFactoryOptions<jiangliUserManager> opeions, IOwinContext context)
        {
            DataDbContext db = context.Get<DataDbContext>();
            jiangliUserManager manager = new jiangliUserManager(new UserStore<M_JiangliUser>(db));
            return manager;
        }
    }
}
