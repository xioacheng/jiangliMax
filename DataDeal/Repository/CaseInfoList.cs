using DataDeal.BLL;
using DataDeal.Models.RepositoryModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Repository
{
    public class CaseInfoList
    {
        CaseService dbCase = new CaseService();
        UserInfo dbUser = new UserInfo();
        FollowCaseService dbFollow = new FollowCaseService();
        CommentService dbComment = new CommentService();
        InvolveService dbInvolve = new InvolveService();
        private static object obj = new object();
        public List<HomeCase> HomeCaseList()
        {
            //var adduser = dbUser.AddUser();
            //dbCase.AddEntity(new Models.M_Case { Title="添加测试案件",StatementOfCase="测试案件的一些声明信息",StateOfCase= WholeSituation.CaseState.PUBLISH ,PublisherId=adduser.Id});
            //需要通过案件获取 用户 关注 评论 裁决
            var caselist = dbCase.LoadEntities(t=>t.StateOfCase == WholeSituation.CaseState.PUBLISH);
            var count = caselist==null?0:caselist.Count();
            if (count == 0)
            {
                return null;
            }
            List<HomeCase> result = new List<HomeCase>();
            foreach(var caseitem in caselist)
            {
                var user = dbUser.GetUserById(caseitem.PublisherId);
                if (user != null)
                {
                    var followList = dbFollow.LoadEntities(t=>t.caseid == caseitem.ID);
                    //评论信息 
                    var comment = dbComment.LoadEntities(t => t.CaseId == caseitem.ID);
                    var involve = dbInvolve.LoadEntities(t=>t.CaseId == caseitem.ID);
                    HomeCase casetemp = new HomeCase
                    {
                        publishid = user.Id,
                        publishname = user.UserName,
                        caseid = caseitem.ID,
                        casetitle = caseitem.Title,
                        casecontent = caseitem.StatementOfCase,
                        followlist = followList,
                        commentlist = comment,
                        involvelist = involve,
                        publicdate = caseitem.DateOfBegin,
                    };
                    result.Add(casetemp);
                }
            }
            return result;
        }
        public UserCase GetUserCase (string UserName)
        {
            //审核
            var shenhelist = dbCase.LoadEntities(t=>t.StateOfCase==WholeSituation.CaseState.PENDING&&t.Publisher==UserName);
            //发布
            var fabulist = dbCase.LoadEntities(t => t.StateOfCase == WholeSituation.CaseState.PUBLISH && t.Publisher == UserName);
            //申诉
            var shensulist = dbCase.LoadEntities(t => t.StateOfCase == WholeSituation.CaseState.COMPLAIN && t.Publisher == UserName);
            //完成
            var wanchenglist = dbCase.LoadEntities(t => t.StateOfCase == WholeSituation.CaseState.FINISH && t.Publisher == UserName);
            //草稿
            var caogaolist = dbCase.LoadEntities(t => t.StateOfCase == WholeSituation.CaseState.DRAFT && t.Publisher == UserName);
            return new UserCase
            {
                shenhelist = shenhelist,
                fabulist = fabulist,
                shensulist = shensulist,
                wanchenglist = wanchenglist,
                caogaolist = caogaolist
            };
        }
    }
}
