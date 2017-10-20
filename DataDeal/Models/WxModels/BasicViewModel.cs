
using DataDeal.WholeSituation;
using System.Linq;

namespace DataDeal.Models.WxModels
{
    public class BasicCaseViewModel : BaseViewModel
    {
        public int id { get; set; }
        public string cover { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string otherPay1 { get; set; }
        public string otherPay2 { get; set; }
        public string otherPay3 { get; set; }
        public IQueryable<M_Comment> numberOfComment { get; set; }
        public IQueryable<M_Involve> numberOfJoin { get; set; }
        public IQueryable<M_FollowCase> numberOfFollow { get; set; }
        /// <summary>
        /// 案件开始的时间
        /// </summary>
        public string start_at { get; set; }
        /// <summary>
        /// 案件的状态
        /// </summary>
        public CaseState state { get; set; }
    }
    /// <summary>
    /// 用户的基本信息
    /// </summary>
    public class BasicUserViewModel
    {
        public string id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 头像的url
        /// </summary>
        public string avatar_url { get; set; }
        public IQueryable<M_Case> numbercase { get; set; }
        public double weight { get; set; }

    }
}