using DataDeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.DAL
{
    public class ContextFactory
    {
        public static DataDbContext GetCurrentDbContext()//获取当前上下文信息DbContext
        {
            DataDbContext dbContext = CallContext.GetData("DbContext") as DataDbContext;
            if(dbContext == null)//如果不存在则创建dbContext
            {
                dbContext = new DataDbContext();
                CallContext.SetData("DbContext",dbContext);
            }
            return dbContext;
        }
    }
}
