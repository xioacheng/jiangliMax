using DataDeal.BLL;
using DataDeal.DAL;
using DataDeal.Infrastructure;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using DataDeal.Models;

namespace DataDeal.Repository
{
    public class BaseTest
    {
        CaseService caseinfo = new CaseService();
        public void update()
        {
            log4net.ILog log = log4net.LogManager.GetLogger("testApp.Logging");//获取一个日志记录器
            log.Info("My Data Here");//写入一条新log
            log.Warn("My Warn Data");
            RoleManager.Create(new M_JiangliRole("admin"));
        }
        private jiangliUserManager UserManager
        {
            get
            {
                return httpContextBase.GetOwinContext().GetUserManager<jiangliUserManager>();
            }
        }
        private jiangliRoleManager RoleManager
        {
            get
            {
                return httpContextBase.GetOwinContext().GetUserManager<jiangliRoleManager>();
            }
        }
        //未将对象引用设置到对象实例
        HttpContextBase httpContextBase
        {
            get
            {
                HttpContextWrapper context = new HttpContextWrapper(HttpContext.Current);
                return context;
            }
        }
    }
}
