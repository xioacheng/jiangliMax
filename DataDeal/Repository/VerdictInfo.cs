using DataDeal.BLL;
using DataDeal.Models;
using DataDeal.Models.RepositoryModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Repository
{
    public class VerdictInfo
    {
        /// <summary>
        /// 获取用户对于案件的裁决信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="caseid"></param>
        /// <returns></returns>
        public IQueryable<M_Involve> GetVerdivt(string username,int caseid)
        {
            UserInfo dbuser = new UserInfo();
            InvolveService dbinvolve = new InvolveService();
            var user = dbuser.GetUseByName(username);
            var verdict = dbinvolve.LoadEntities(t=>t.CaseId==caseid&&t.UserID==user.Id);
            return verdict;
        }
        /// <summary>
        /// 用户参与裁决
        /// </summary>
        /// <returns></returns>
        public int AddVerdict(string username,Verdict model)
        {
            InvolveService dbinvolve = new InvolveService();
            UserInfo dbuser = new UserInfo();
            string userId = dbuser.GetUseByName(username).Id;
            GetPoint dbpoint = new GetPoint();
            //查找数据库中是否已经存在裁决
            var VerdictList = dbinvolve.LoadEntities(t => t.CaseId == model.caseid && t.UserID == userId);
            //必须赔偿
            if (model.styleOfpay == "1")
            {   
                if (VerdictList.Count() > 0)
                {
                    //存在的话进行更新
                    var UserVerdict = VerdictList.FirstOrDefault();
                    UserVerdict.UserID = userId;
                    UserVerdict.CaseId = model.caseid;
                    UserVerdict.AssignmentExplain = model.reason;
                    UserVerdict.DoublePay = Convert.ToDouble(model.doublepay);
                    UserVerdict.ModeOfPay = int.Parse(model.styleOfpay);
                    UserVerdict.MostSatisfied = Convert.ToDouble(model.moneySure);
                    UserVerdict.NonSatisfied = Convert.ToDouble(model.nopay);
                    UserVerdict.OtherNum1 = Convert.ToDouble(model.OtherNum1);
                    UserVerdict.OtherNum2 = Convert.ToDouble(model.OtherNum2);
                    UserVerdict.OtherNum3 = Convert.ToDouble(model.OtherNum3);
                    //添加曲线点列表
                    string pointlist = dbpoint.GetSinglePoint(UserVerdict);
                    UserVerdict.PointList = pointlist;
                    var result = dbinvolve.UpdateEntity(UserVerdict);
                    UserVerdict.ModifiedDate = DateTime.Now;
                    if (result)
                    {
                        //添加裁决成功后判断案件的裁决，决定是不是将案件的状态修改为已完成
                        int count = dbinvolve.LoadEntities(t => t.CaseId == model.caseid).Count();
                        CaseInfo dbcase = new CaseInfo();
                        if (count >= WholeSituation.GlobalConfig.InvolveCount)
                        {
                            int resultTemp = dbcase.UpdateCaseState(model.caseid,WholeSituation.CaseState.FINISH);
                        }
                        return 1;
                    }
                    return -1;
                }
                else
                {
                    //不存在裁决的话 直接添加裁决
                    //赔偿金额 no满意度 double满意度 reason
                    M_Involve temp = new M_Involve
                    {
                        UserID = userId,
                        CaseId = model.caseid,
                        AssignmentExplain = model.reason,
                        DoublePay = Convert.ToDouble(model.doublepay),
                        ModeOfPay = int.Parse(model.styleOfpay),
                        MostSatisfied = Convert.ToDouble(model.moneySure),
                        NonSatisfied = Convert.ToDouble(model.nopay),
                        ParticipateDate = DateTime.Now,
                        OtherNum1 = Convert.ToDouble(model.OtherNum1),
                        OtherNum2 = Convert.ToDouble(model.OtherNum2),
                        OtherNum3 = Convert.ToDouble(model.OtherNum3),
                };
                    //添加曲线点列表
                    string pointlist = dbpoint.GetSinglePoint(temp);
                    temp.PointList = pointlist;
                    var result = dbinvolve.AddEntity(temp);
                    if (result.ID != 0)
                    {
                        //添加裁决成功后判断案件的裁决，决定是不是将案件的状态修改为已完成
                        int count = dbinvolve.LoadEntities(t => t.CaseId == model.caseid).Count();
                        CaseInfo dbcase = new CaseInfo();
                        if (count >= WholeSituation.GlobalConfig.InvolveCount)
                        {
                            int resultTemp = dbcase.UpdateCaseState(model.caseid, WholeSituation.CaseState.FINISH);
                        }
                        return 1;
                    }
                    else return 0;
                }
                
            }
            //互不赔偿
            else if (model.styleOfpay == "2")
            {
                if (VerdictList.Count() > 0)
                {
                    //存在的话进行更新
                    var UserVerdict = VerdictList.FirstOrDefault();
                    UserVerdict.UserID = userId;
                    UserVerdict.CaseId = model.caseid;
                    UserVerdict.AssignmentExplain = model.reason;
                    UserVerdict.DoublePay = Convert.ToDouble(model.doublepay);
                    UserVerdict.ModeOfPay = int.Parse(model.styleOfpay);
                    UserVerdict.MostSatisfied = 0;
                    UserVerdict.NonSatisfied = 10;
                    UserVerdict.ModifiedDate = DateTime.Now;
                    UserVerdict.OtherNum1 = Convert.ToDouble(model.OtherNum1);
                    UserVerdict.OtherNum2 = Convert.ToDouble(model.OtherNum2);
                    UserVerdict.OtherNum3 = Convert.ToDouble(model.OtherNum3);
                    //添加曲线点列表
                    string pointlist = dbpoint.GetSinglePoint(UserVerdict);
                    UserVerdict.PointList = pointlist;
                    var result2 = dbinvolve.UpdateEntity(UserVerdict);
                    if (result2)
                    {
                        //添加裁决成功后判断案件的裁决，决定是不是将案件的状态修改为已完成
                        int count = dbinvolve.LoadEntities(t => t.CaseId == model.caseid).Count();
                        CaseInfo dbcase = new CaseInfo();
                        if (count >= WholeSituation.GlobalConfig.InvolveCount)
                        {
                            int resultTemp = dbcase.UpdateCaseState(model.caseid, WholeSituation.CaseState.FINISH);
                        }
                        return 1;
                    }
                    return -1;
                }
                else
                {
                    //赔偿金额0 no满意度10 double满意度 reason 
                    M_Involve temp = new M_Involve
                    {
                        UserID = userId,
                        CaseId = model.caseid,
                        AssignmentExplain = model.reason,
                        DoublePay = Convert.ToDouble(model.doublepay),
                        ModeOfPay = int.Parse(model.styleOfpay),
                        MostSatisfied = 0,
                        NonSatisfied = 10,
                        ParticipateDate = DateTime.Now,
                        OtherNum1 = Convert.ToDouble(model.OtherNum1),
                        OtherNum2 = Convert.ToDouble(model.OtherNum2),
                        OtherNum3 = Convert.ToDouble(model.OtherNum3),
                    };
                    // 添加曲线点列表
                    string pointlist = dbpoint.GetSinglePoint(temp);
                    temp.PointList = pointlist;
                    var result = dbinvolve.AddEntity(temp);
                    if (result.ID != 0)
                    {
                        //添加裁决成功后判断案件的裁决，决定是不是将案件的状态修改为已完成
                        int count = dbinvolve.LoadEntities(t => t.CaseId == model.caseid).Count();
                        CaseInfo dbcase = new CaseInfo();
                        if (count >= WholeSituation.GlobalConfig.InvolveCount)
                        {
                            int resultTemp = dbcase.UpdateCaseState(model.caseid, WholeSituation.CaseState.FINISH);
                        }
                        return 1;
                    }
                    else return 0;
                }
            }
            else
            {
                return -1;
            }
        }
    }
}
