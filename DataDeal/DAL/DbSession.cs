using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.DAL
{
    public class DbSession
    {
        public static int SaveChanges()
        {
            //获取当前DbContext进行数据保存
            return DAL.ContextFactory.GetCurrentDbContext().SaveChanges();
        }
    }
}
