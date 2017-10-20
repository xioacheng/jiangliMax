using DataDeal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Infrastructure
{
    public class jiangliRoleManager:RoleManager<M_JiangliRole>
    {
        public jiangliRoleManager(RoleStore<M_JiangliRole> store) : base(store) { }
        public static jiangliRoleManager Create(IdentityFactoryOptions<jiangliRoleManager> options,IOwinContext context)
        {
            return new jiangliRoleManager(new RoleStore<M_JiangliRole>(context.Get<DataDbContext>()));
        }
    }
}
