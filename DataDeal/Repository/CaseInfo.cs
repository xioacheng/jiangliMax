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
    public class CaseInfo
    {
        /// <summary>
        /// 修改案件状态
        /// </summary>
        /// <param name="caseid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateCaseState(int caseid,WholeSituation.CaseState state)
        {
            CaseService dbcase = new CaseService();
            var item = dbcase.LoadEntities(t=>t.ID==caseid).FirstOrDefault();
            item.StateOfCase = state;
            var result = dbcase.UpdateEntity(item);
            if (result)
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// 通过案件id删除案件
        /// </summary>
        /// <param name="caseid"></param>
        /// <returns></returns>
        public int DeleteCaseById(int caseid)
        {
            //1删除成功 0删除失败
            CaseService dbcase = new CaseService();
            var result = dbcase.DeleteEntity(new M_Case { ID=caseid});
            if (result)
            {
                return 1;
            }
            return 0;
        }
        public M_Case GetCaseById(int caseid)
        {
            CaseService dbcase = new CaseService();
            var caselist = dbcase.LoadEntities(t=>t.ID==caseid);
            return caselist.FirstOrDefault();
        }
        public IQueryable<M_Case> GetCaseById2(int caseid)
        {
            CaseService dbcase = new CaseService();
            var caselist = dbcase.LoadEntities(t => t.ID == caseid);
            return caselist;
        }
        /// <summary>
        /// 发布案件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int PublishCase(casepublist model)
        {
            CaseService dbcase = new CaseService();
            //应诉人是站内的用户
            if (model.respondentid != null)
            {
                M_Case caseitem = new M_Case
                {
                    CaseOfSubmit = DateTime.Now,
                    Complainant = model.publishname,
                    ComplainantId = model.publishid,
                    DateOfBegin = DateTime.Now,
                    DateOfEnd = DateTime.Now.AddDays(10),
                    originalpay = int.Parse(model.paynum),
                    Publisher = model.publishname,
                    PublisherId = model.publishid,
                    Respondent = model.respondent,
                    RespondentId = model.respondentid,
                    StatementOfCase = model.content,
                    StateOfCase = model.savemod=="保存"?WholeSituation.CaseState.DRAFT:WholeSituation.CaseState.PUBLISH,
                    Title = model.casetitle,
                    OtherPay1 = model.paymod1,
                    OtherPay2 = model.paymod2,
                    OtherPay3 = model.paymod3,
                };
                var result = dbcase.AddEntity(caseitem);
                if(result.ID != 0)
                {
                    return 1;
                }
            }
            //应诉人是手写的用户 没有应诉人id
            else
            {
                M_Case caseitem = new M_Case
                {
                    CaseOfSubmit = DateTime.Now,
                    Complainant = model.publishid,
                    ComplainantId = model.publishid,
                    DateOfBegin = DateTime.Now,
                    DateOfEnd = DateTime.Now.AddDays(10),
                    originalpay = int.Parse(model.paynum),
                    Publisher = model.publishname,
                    PublisherId = model.publishid,
                    Respondent = model.respondent,
                    RespondentEmail = model.respondentEmail,
                    RespondentAddress = model.respondentAddress,
                    RespondentPhone = model.respondentPhone,
                    StatementOfCase = model.content,
                    StateOfCase = model.savemod == "保存" ? WholeSituation.CaseState.DRAFT : WholeSituation.CaseState.PUBLISH,
                    Title = model.casetitle,
                    OtherPay1 = model.paymod1,
                    OtherPay2 = model.paymod2,
                    OtherPay3 = model.paymod3,
                };
                var result = dbcase.AddEntity(caseitem);
                if (result.ID != 0)
                {
                    return 1;
                }
            }
            return 1;
        }
        /// <summary>
        /// 公众获取案件信息
        /// </summary>
        /// <param name="caseid"></param>
        /// <returns></returns>
        public dynamic PublicCase(int caseid)
        {
            //通过caseid获取 用户 裁决 评论 
            CaseService dbcase = new CaseService();
            InvolveService dbinvolve = new InvolveService();
            CommentService dbcomment = new CommentService();
            UserInfo dbuser = new UserInfo();
            FollowCaseService dbfollow = new FollowCaseService();
            var casetemp =  dbcase.LoadEntities(t=>t.ID==caseid);
            if (casetemp == null) return null;
            var caseitem = casetemp.FirstOrDefault();
            if(caseitem.StateOfCase!=WholeSituation.CaseState.PUBLISH&& caseitem.StateOfCase != WholeSituation.CaseState.FINISH)
            {
                return null;
            }
            else
            {
                var follow = dbfollow.LoadEntities(t=>t.caseid==caseitem.ID);
                //评论信息
                var verdict = dbinvolve.LoadEntities(t => t.CaseId == caseitem.ID);
                var comment = dbcomment.LoadEntities(t => t.CaseId == caseitem.ID).OrderByDescending(t=>t.Date); ;
                publiccaseitem result = new publiccaseitem
                {
                    publishid = caseitem.PublisherId,
                    publishname = caseitem.Publisher,
                    userhead = dbuser.GetUserById(caseitem.PublisherId)==null?"": dbuser.GetUserById(caseitem.PublisherId).Avatar_url,
                    respondentid = caseitem.RespondentId,
                    respondentname = caseitem.Respondent,
                    respondenthead = dbuser.GetUserById(caseitem.RespondentId)==null?"": dbuser.GetUserById(caseitem.PublisherId).Avatar_url,
                    casestate = caseitem.StateOfCase,
                    publishdata = caseitem.DateOfBegin,
                    follow = follow,
                    verdict = verdict,
                    comment = comment,
                    caseid = caseitem.ID,
                    casetitle = caseitem.Title,
                    casecontent = caseitem.StatementOfCase,
                    caseinfo = caseitem,
                };
                return result;
            }
        }
    }
}
