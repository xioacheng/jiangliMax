using DataDeal.BLL;
using DataDeal.Models;
using DataDeal.Models.WxModels;
using DataDeal.Models.WxModels.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Repository.WxRepository
{
    public class CaseInfo
    {
        private static CaseInfo caseinfo = new CaseInfo();
        public static CaseInfo Current { get { return caseinfo; } }
        private CaseService dbcase = new CaseService();
        private InvolveService dbpart = new InvolveService();
        private UserInfo dbuser = new UserInfo();
        private CommentService dbcomment = new CommentService();
        private FollowCaseService dbfollow = new FollowCaseService();
        public bool Create(WxPostCaseViewModel model)
        {
            CaseService dbcase = new CaseService();
            M_Case caseitem = new M_Case()
            {
                CaseOfSubmit = DateTime.Now,
                Complainant = model.accuser,
                ComplainantId = model.accuserid,
                DateOfBegin = DateTime.Now,
                DateOfEnd = DateTime.Now.AddDays(10),
                originalpay = model.originalpay,
                Publisher = model.accuser,
                PublisherId = model.accuserid,
                Respondent = model.respondent,
                RespondentId = model.respondentid,
                StatementOfCase = model.description,
                StateOfCase = model.state,
                Title = model.title,
                OtherPay1 = model.otherPay1,
                OtherPay2 = model.otherPay2,
                OtherPay3 = model.otherPay3,
            };
            var result = dbcase.AddEntity(caseitem);
            if (result.ID != 0)
            {
                return true;
            }
            return false;
        }
        public M_Case GetCaseByid(int caseid)
        {
            var caseinfo = dbcase.LoadEntities(t => t.ID == caseid).FirstOrDefault();
            return caseinfo;
        }
        public bool isExist(int id)
        {
            var caselist = dbcase.LoadEntities(t => t.ID == id);
            if (caselist.Count() > 0) return true;
            return false;
        }
        public  WxCaseDetailViewModel _getCaseDetailById(int id)
        {
            BasicUserViewModel respondent = new BasicUserViewModel();
            M_Case caseinfo = dbcase.LoadEntities(t => t.ID == id).FirstOrDefault();
            var parts = dbpart.LoadEntities(t=>t.CaseId==id);
            if (caseinfo.RespondentId != null)
                respondent = dbuser.GetBasicUserById(caseinfo.RespondentId);
            else
                respondent = new BasicUserViewModel()
                {
                    nickname = caseinfo.Respondent,
                };
            var owner = dbuser.GetBasicUserById(caseinfo.PublisherId);
            return new WxCaseDetailViewModel()
            {
                user = owner,
                basic = new BasicCaseViewModel()
                {
                    description = caseinfo.StatementOfCase,
                    id = caseinfo.ID,
                    numberOfComment = dbcomment.LoadEntities(t => t.CaseId == id),
                    title = caseinfo.Title,
                    start_at = caseinfo.DateOfBegin.ToShortDateString(),
                    state = caseinfo.StateOfCase,
                    numberOfJoin = dbpart.LoadEntities(t => t.CaseId == id),
                    numberOfFollow = dbfollow.LoadEntities(t=>t.caseid==id),
                    otherPay1 = caseinfo.OtherPay1,
                    otherPay2 = caseinfo.OtherPay2,
                    otherPay3 = caseinfo.OtherPay3,
                },
                imageSrc = caseinfo.Photo,
                involves = PartInfo.Current.GetPartsByCaseId(id),
                respondent = respondent,
                respondentturnone = caseinfo.TrunOneComplainByRespondent ?? "",
                respondentturntwo = caseinfo.TrunTwoComplainByRespondent ?? "",
                userturnone = caseinfo.TrunOneComplainByComplainant ?? "",
                userturntwo = caseinfo.TrunTwoComplainByComplainant ?? "",
                condition = caseinfo.Condition,
                orginalpay = caseinfo.originalpay,
            };
        }
    }
}
