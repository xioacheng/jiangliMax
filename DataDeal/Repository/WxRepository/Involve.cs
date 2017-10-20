using DataDeal.BLL;
using DataDeal.Models.RepositoryModal;
using DataDeal.Models.WxModels.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Repository.WxRepository
{
    public class Involve
    {
        private static Involve involve = new Involve();
        public  static Involve Current { get { return involve; } }
        private InvolveService dbpart = new InvolveService();
        public string GetPartPoint(int pid)
        {
            return dbpart.LoadEntities(t => t.ID == pid).FirstOrDefault().PointList;
        }
        public bool AddInvolve(WxPostInvolveViewModel model)
        {
            VerdictInfo dbverdict = new VerdictInfo();
            string userid = model.userid;
            string username = UserInfo.Current.GetUserById(userid).UserName;
            //1是单点赋值 2是三点赋值 输入金额为0是满意度为10（元金额赔偿满意度为doublepay）
            //stylepay 1是必须赔偿 2是互不赔偿
            string  stylepay = "2";
            if (model.modepay == 2)
            {
                stylepay = "1";
            }
            if (model.modepay == 1)
            {
                stylepay = "2";
            }
            Verdict verdict = new Verdict
            {
                caseid = model.caseid,
                doublepay = model.doublePay.ToString(),
                moneySure = model.mostSatisfied.ToString(),
                nopay = model.nonSatisfied.ToString(),
                OriginalPay = model.orginalpay.ToString(),
                OtherNum1 = model.otherSatisfied1.ToString(),
                OtherNum2 = model.otherSatisfied2.ToString(),
                OtherNum3 = model.otherSatisfied3.ToString(),
                reason = model.assignmentExplain,
                styleOfpay = stylepay,
            };
            var result = dbverdict.AddVerdict(username,verdict);
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}
