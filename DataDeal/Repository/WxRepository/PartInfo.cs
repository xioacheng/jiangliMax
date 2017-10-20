using DataDeal.BLL;
using DataDeal.Models;
using DataDeal.Models.WxModels.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Repository.WxRepository
{
    public class PartInfo
    {
        private static PartInfo part = new PartInfo();
        public static PartInfo Current { get { return part; } }
        private InvolveService dbpart = new InvolveService();
        /// <summary>
        /// 通过案件Id获得裁决列表
        /// </summary>
        /// <param name="id">案件id</param>
        /// <returns></returns>
        public IEnumerable<WxInvolveViewModel> GetPartsByCaseId(int id)
        {
            var parts = dbpart.LoadEntities(t=>t.CaseId==id);
            List<WxInvolveViewModel> result = new List<WxInvolveViewModel>();
            foreach (var item in parts)
            {
                result.Add(_transModel(item));
            }
            return result;
        }
        #region 内部方法
        protected WxInvolveViewModel _transModel(M_Involve item)
        {
            return new WxInvolveViewModel()
            {
                id = item.ID,
                assignmentExplain = item.AssignmentExplain,
                doublePay = item.DoublePay,
                ParticipateDate = item.ParticipateDate.ToShortDateString(),
                modepay = item.ModeOfPay,
                mostSatisfied = item.MostSatisfied,
                nonSatisfied = item.NonSatisfied,
                user = UserInfo.Current.GetBasicUserById(item.UserID),
                modedot = item.ModeOfDot,
                other1 = item.OtherNum1,
                other2 = item.OtherNum2,
                other3 = item.OtherNum3,
            };
        }
        #endregion
    }
}
