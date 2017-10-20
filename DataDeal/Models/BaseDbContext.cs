using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models
{
    //继承IdentityDbContext作为DbContext的基类
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BaseDbContext:IdentityDbContext
    {
        public BaseDbContext() : base("jianglidata") { }
    }
}
