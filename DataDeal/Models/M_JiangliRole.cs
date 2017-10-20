using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models
{
    //角色列表，可以增加自己的属性
    public class M_JiangliRole:IdentityRole
    {
        public M_JiangliRole() : base() { }

        public M_JiangliRole(string name) : base(name) { }
    }
}
