using DataDeal.Models;
using DataDeal.Models.WxModels.Case;
using DataDeal.Repository;
using DataDeal.Repository.WxRepository;
using DataDeal.WholeSituation;
using jiangliMax.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace jiangliMax.Controllers.WebApi
{
    [WxAuthorize]
    public class CaseAPIController : ApiController
    {
        /// <summary>
        /// 通过关键词查找案件列表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="q"></param>
        /// <param name="page"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        public List<dynamic> GetPieceByKeyWord(int limit, string q, int page, CaseState state)
        {
            var caselist = CaseList.Current.GetPieceByPageAndWordAndState(q, state, page);
            return _MainCaseList(caselist);
        }
        /// <summary>
        /// 创建一个案件
        /// </summary>
        /// <param name="newCase"></param>
        /// <returns></returns>
        [HttpPost]
        public bool CreateCase(WxPostCaseViewModel newCase)
        {
            var result = DataDeal.Repository.WxRepository.CaseInfo.Current.Create(newCase);
            return result;
        }
        /// <summary>
        /// 获取所有已发布的案件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<dynamic> GetPieceByPageAndState(int page, CaseState state)
        {
            CaseList dbcase = new CaseList();
            var caselist = dbcase.GetPieceByPagesAndState(page, state).OrderByDescending(t=>t.basic.numberOfComment.Count());
            return _MainCaseList(caselist);
        }
        public dynamic GetCaseDetailById(int caseId)
        {
            DataDeal.Repository.WxRepository.CaseInfo dbcase = new DataDeal.Repository.WxRepository.CaseInfo();
            var caseinfo = dbcase._getCaseDetailById(caseId);
            return _CaseInfo(caseinfo);
        }
        public IEnumerable<WxCommentViewModel> GetCommentsByCaseId(int cid, int page)
        {
            return DataDeal.Repository.WxRepository.Comment.current.GetCommentsByCaseId(cid, page);
        }
        public WxCommentViewModel CreateComment(WxCommentViewModel model)
        {
            // 判断用户名和案件是否存在
            // 判断用户是否合法
            if (UserInfo.Current.GetUserById(model.uid)==null)
            {
                return null;
            }
            if (!DataDeal.Repository.WxRepository.CaseInfo.Current.isExist(model.caseid))
            {
                return null;
            }
            if (model.content == null)
            {
                return null;
            }
            var comment = DataDeal.Repository.WxRepository.Comment.current.m_create(new M_Comment
            {
                CaseId = model.caseid,
                UserID = model.uid,
                UserName = model.username,
                Content = model.content,
                Date = DateTime.Now,
            });
            return new WxCommentViewModel()
            {
                caseid = comment.CaseId,
                content = comment.Content,
                created_at = comment.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                id = comment.ID,
                pid = "0",
                pusername = "achang",
                uid = comment.UserID,
                username = comment.UserName,
                avatar_url = UserInfo.Current.GetUserById(comment.UserID).Avatar_url,
            };
        }
        /// <summary>
        /// 创建一个裁决
        /// </summary>
        [HttpPost]
        public bool CreateInvolve(WxPostInvolveViewModel model)
        {
            var result = Involve.Current.AddInvolve(model);
            return result;
        }
        /// <summary>
        /// 修改一个裁决
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public bool ModifiedInvolve(WxPostInvolveViewModel model)
        {
            var result = Involve.Current.AddInvolve(model);
            return result;
        }
        #region 内部方法
        /// <summary>
        /// 删除html文本格式
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(string html, int length = 0)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);

            return strText;
        }
        public dynamic _CaseInfo(WxCaseDetailViewModel caseinfo)
        {
            var temp = new
            {
                basic = new
                {
                    avatar_url = caseinfo.basic.avatar_url,
                    cover = caseinfo.basic.cover,
                    description = Regex.Replace(caseinfo.basic.description, @"<img\b[^>]*>", ""),
                    id = caseinfo.basic.id,
                    numberOfComment = caseinfo.basic.numberOfComment.Count(),
                    numberOfJoin = caseinfo.basic.numberOfJoin.Count(),
                    numberOfFollow = caseinfo.basic.numberOfFollow.Count(),
                    RoleName = caseinfo.basic.RoleName,
                    start_at = caseinfo.basic.start_at,
                    state = caseinfo.basic.state,
                    title = caseinfo.basic.title,
                    userId = caseinfo.basic.userId,
                    userName = caseinfo.basic.userName,
                    otherpay1 = caseinfo.basic.otherPay1,
                    otherpay2 = caseinfo.basic.otherPay2,
                    otherpay3 = caseinfo.basic.otherPay3,
                },
                condition = caseinfo.condition,
                imageSrc = caseinfo.imageSrc,
                involves = caseinfo.involves,
                orginalpay = caseinfo.orginalpay,
                respondent = new
                {
                    avatar_url = caseinfo.respondent.avatar_url == null ? "": caseinfo.respondent.avatar_url,
                    id=caseinfo.respondent.id==null?"": caseinfo.respondent.id,
                    nickname=caseinfo.respondent.nickname==null?"": caseinfo.respondent.nickname,
                    numbercase=caseinfo.respondent.numbercase==null?0: caseinfo.respondent.numbercase.Count(),
                    weight=caseinfo.respondent.weight
                },
                respondentturnone = caseinfo.respondentturnone,
                respondentturntwo = caseinfo.respondentturntwo,
                user = new
                {
                    avatar_url = caseinfo.user.avatar_url,
                    id = caseinfo.user.id,
                    nickname = caseinfo.user.nickname,
                    numbercase=caseinfo.user.numbercase.Count(),
                    weight=caseinfo.user.weight,
                },
                userturnone = caseinfo.userturnone,
                userturntwo = caseinfo.userturntwo,
            };
            return temp;
        }
        /// <summary>
        /// MainCaselist数据格式转化
        /// </summary>
        /// <param name="caselist"></param>
        /// <returns></returns>
        [NonAction]
        public List<dynamic> _MainCaseList(IQueryable<WxCaseListItemViewModel> caselist)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (var item in caselist)
            {
                var temp = new
                {
                    basic = new
                    {
                        avatar_url = item.basic.avatar_url,
                        cover = item.basic.cover,
                        description = item.basic.description,
                        id = item.basic.id,
                        numberOfComment = item.basic.numberOfComment.Count(),
                        numberOfJoin = item.basic.numberOfJoin.Count(),
                        numberOfFollow = item.basic.numberOfFollow.Count(),
                        RoleName = item.basic.RoleName,
                        start_at = item.basic.start_at,
                        state = item.basic.state,
                        title = item.basic.title,
                        userId = item.basic.userId,
                        userName = item.basic.userName,
                    },
                    author = new
                    {
                        avatar_url = item.author.avatar_url,
                        id = item.author.id,
                        nickname = item.author.nickname,
                        numbercase = item.author.numbercase.Count(),
                        weight = item.author.weight,
                    },
                };
                result.Add(temp);
            }
            return result;
        }
        #endregion
    }
}
