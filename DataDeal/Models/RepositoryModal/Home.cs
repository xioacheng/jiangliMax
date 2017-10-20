using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models.RepositoryModal
{
    /// <summary>
    /// 个人主页
    /// </summary>
    public class HomePerson
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string hobby { get; set; }
        public string headimg { get; set; }
        public IQueryable<M_Case> publishlist { get; set; }
        public IQueryable<M_Case> finishlist { get; set; }
        public List<IQueryable<M_Case>> followlist { get; set; }
        public List<IQueryable<M_Case>> participatelist { get; set; }
        public double weight { get; set; }
    }
    /// <summary>
    /// 公共案件信息类
    /// </summary>
    public class publiccaseitem
    {
        /// <summary>
        /// 发布者id
        /// </summary>
        public string publishid { get; set; }
        /// <summary>
        /// 发布者姓名
        /// </summary>
        public string publishname { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string userhead { get; set; }
        /// <summary>
        /// 应诉人id
        /// </summary>
        public string respondentid { get; set; }
        /// <summary>
        /// 应诉人姓名
        /// </summary>
        public string respondentname { get; set; }
        /// <summary>
        /// 应诉人头像
        /// </summary>
        public string respondenthead { get; set; }
        public WholeSituation.CaseState casestate { get; set; }
        public DateTime publishdata { get; set; }
        public IQueryable<M_FollowCase> follow { get; set; }
        public IQueryable<M_Involve> verdict { get; set; }
        public IQueryable<M_Comment> comment { get; set; }
        public int caseid { get; set; }
        public string casetitle { get; set; }
        public string casecontent { get; set; }
        public M_Case caseinfo { get; set; }
    }
    /// <summary>
    /// 首页案件列表
    /// </summary>
    public class HomeCase
    {
        public string publishid { get; set; }
        public string publishname { get;set; }
        public string publishavatar { get; set; }
        public int caseid { get; set; }
        public string casetitle { get; set; }
        public string casecontent { get; set; }
        public IQueryable<M_FollowCase> followlist { get; set; }
        public IQueryable<M_Comment> commentlist { get; set; }
        public IQueryable<M_Involve> involvelist { get; set; }
        public DateTime publicdate { get; set; }
    }
    /// <summary>
    /// 案件发布列表
    /// </summary>
    public class casepublist
    {
        /// <summary>
        /// 案件标题
        /// </summary>
        public string casetitle { get; set; }
        /// <summary>
        /// 发布人id
        /// </summary>
        public string publishid { get; set; }
        /// <summary>
        /// 发布人用户名
        /// </summary>
        public string publishname { get; set; }
        /// <summary>
        /// 应诉人id
        /// </summary>
        public string respondentid { get; set; }
        /// <summary>
        /// 应诉人用户名
        /// </summary>
        public string respondent { get; set; }
        /// <summary>
        /// 应诉人电话
        /// </summary>
        public string respondentPhone { get; set; }
        /// <summary>
        /// 应诉人邮箱
        /// </summary>
        public string respondentEmail { get; set; }
        /// <summary>
        /// 应诉人地址
        /// </summary>
        public string respondentAddress { get; set; }
        /// <summary>
        /// 应诉人其他说明
        /// </summary>
        public string respondentExplain { get; set; }
        /// <summary>
        /// 赔偿金额
        /// </summary>
        public string paynum { get; set; }
        /// <summary>
        /// 额外赔偿方式1
        /// </summary>
        public string paymod1 { get; set; }
        /// <summary>
        /// 额外赔偿方式2
        /// </summary>
        public string paymod2 { get; set; }
        /// <summary>
        /// 额外赔偿方式3
        /// </summary>
        public string paymod3 { get; set; }
        /// <summary>
        /// 案件说明
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 保存方式
        /// </summary>
        public string savemod { get; set; }
    }
    /// <summary>
    /// 案件裁决
    /// </summary>
    public class Verdict
    {
        public int caseid { get; set; }
        /// <summary>
        /// 1 必须赔偿 2 互不赔偿
        /// </summary>
        public string styleOfpay { get; set; }
        /// <summary>
        /// 最满意赔偿金额
        /// </summary>
        public string moneySure { get; set; }
        /// <summary>
        /// 不赔偿时候的满意金额
        /// </summary>
        public string nopay { get; set; }
        /// <summary>
        /// 双倍赔偿时候的满意金额
        /// </summary>
        public string doublepay { get; set; }
        /// <summary>
        /// 原价赔偿时候的满意度
        /// </summary>
        public string OriginalPay { get; set; }
        /// <summary>
        /// 赋值理由
        /// </summary>
        public string reason { get; set; }
        public string OtherNum1 { get; set; }
        public string OtherNum2 { get; set; }
        public string OtherNum3 { get; set; }
    }
}
