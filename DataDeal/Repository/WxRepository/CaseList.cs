using DataDeal.BLL;
using DataDeal.Models;
using DataDeal.Models.WxModels;
using DataDeal.Models.WxModels.Case;
using DataDeal.WholeSituation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataDeal.Repository.WxRepository
{
    public class CaseList
    {
        public static CaseList caselist = new CaseList();
        public static CaseList Current { get { return caselist; } }
        CaseService dbcase = new CaseService();
        CommentService dbcomment = new CommentService();
        InvolveService dbpart = new InvolveService();
        FollowCaseService dbfollow = new FollowCaseService();
        /// <summary>
        /// 通过页数和状态获取20条案件
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public IQueryable<WxCaseListItemViewModel> GetPieceByPagesAndState(int page, CaseState state)
        {
            int total = 0;
            int skipcount = (page - 1) * 20;
            var currentlist = state != CaseState.ALL ? dbcase.LoadPageEntities(page, 10, out total, r => r.StateOfCase == state, false, r => r.ID) :
                dbcase.LoadEntities(t => t.ID != 0);
            return _getCardCaseByCases(currentlist).AsQueryable();
        }
        /// <summary>
        /// 搜索案件
        /// </summary>
        /// <param name="q"></param>
        /// <param name="state"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IQueryable<WxCaseListItemViewModel> GetPieceByPageAndWordAndState(string q, CaseState state, int page)
        {

            if (q == null)
            {
                return GetPieceByPagesAndState(page, state);
            }
            var currentlist = m_getCasesByKeyWord(q, page, state);
            return _getCardCaseByCases(currentlist).AsQueryable();
        }
        /// <summary>
        /// 通过关键字查和案件状态找案件
        /// </summary>
        /// <returns></returns>
        public IQueryable<M_Case> m_getCasesByKeyWord(string word, int page, CaseState state)
        {
            //return caseinfo.b_getCaseByKey(word);
            int skipcount = (page - 1) * 20;
            return state == CaseState.ALL ?
                dbcase.LoadEntities(r => r.Title.Contains(word)).AsQueryable() :
                dbcase.LoadEntities(r => r.StateOfCase == state && r.Title.Contains(word)).AsQueryable();
            //var temp = caseinfo.LoadEntities(t => t.Title.Contains(word)&&t.StateOfCase==state).AsQueryable();
        }
        public IQueryable<M_Case> m_getCasesByUserid(string userid)
        {
            List<M_Case> result = new List<M_Case>();
            var caselist = dbcase.LoadEntities(t => t.PublisherId == userid || t.RespondentId == userid);
            foreach (var item in caselist)
            {
                item.StatementOfCase = Regex.Replace(item.StatementOfCase, @"<img\b[^>]*>", "");
                result.Add(item);
            }
            return result.AsQueryable();
            //return caseinfo.LoadEntities(t => t.PublisherId == userid);
        }
        public IQueryable<BasicCaseViewModel> getCaseByUserIdBreif(string userid)
        {
            return _getBasicCaseList(m_getCasesByUserid(userid));
        }
        #region 内部方法
        private IQueryable<BasicCaseViewModel> _getBasicCaseList(IQueryable<M_Case> list)
        {
            List<BasicCaseViewModel> result = new List<BasicCaseViewModel>();
            foreach (var item in list)
            {
                item.StatementOfCase = Regex.Replace(item.StatementOfCase, "<[^>]+>", "");
                item.StatementOfCase = Regex.Replace(item.StatementOfCase, "&[^;]+;", "");
                result.Add(_getBasicCase(item));
            }
            return result.AsQueryable();
        }
        /// <summary>
        /// 通过caselist 获取传送给微信的数据格式
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<WxCaseListItemViewModel> _getCardCaseByCases(IQueryable<M_Case> list)
        {
            List<WxCaseListItemViewModel> cardCaseList = new List<WxCaseListItemViewModel>();
            foreach (var item in list)
            {

                cardCaseList.Add(new WxCaseListItemViewModel()
                {

                    basic = _getBasicCase(item),
                    author = UserInfo.Current.GetBasicUserById(item.PublisherId),

                });
            }
            return cardCaseList;
        }
        /// <summary>
        /// 将Case原始模型转换成微信所需的格式
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private BasicCaseViewModel _getBasicCase(M_Case c)
        {
            return new BasicCaseViewModel()
            {
                id = c.ID,
                cover = "",
                description = c.StatementOfCase,
                numberOfComment = dbcomment.LoadEntities(t=>t.CaseId==c.ID),
                numberOfJoin = dbpart.LoadEntities(t=>t.CaseId==c.ID),
                numberOfFollow = dbfollow.LoadEntities(t=>t.caseid==c.ID),
                start_at = c.DateOfBegin.ToShortDateString(),
                state = c.StateOfCase,
                title = c.Title,
            };
        }
        #endregion
    }
}
