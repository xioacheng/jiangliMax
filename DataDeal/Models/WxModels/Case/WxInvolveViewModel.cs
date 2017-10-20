using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDeal.Models.WxModels.Case
{
    /// <summary>
    /// 返回给微信的评价数据格式
    /// </summary>
    public class WxInvolveViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 参与者Id
        /// </summary>
        public BasicUserViewModel user { get; set; }

        /// <summary>
        /// 赔付方式
        /// </summary>
        public int modepay { get; set; }
        /// <summary>
        /// 赋值方式
        /// </summary>
        public int modedot { get; set; }
        /// <summary>
        /// 最满意的数额
        /// </summary>
        public double mostSatisfied { get; set; }
        /// <summary>
        /// 不赔偿的满意度
        /// </summary>
        public double nonSatisfied { get; set; }
        /// <summary>
        /// 两倍赔偿的满意度
        /// </summary>
        public double doublePay { get; set; }
        /// <summary>
        /// 额外赔偿方式1的满意度
        /// </summary>
        public double other1 { get; set; }
        /// <summary>
        /// 额外赔偿方式1的满意度
        /// </summary>
        public double other2 { get; set; }
        /// <summary>
        /// 额外赔偿方式1的满意度
        /// </summary>
        public double other3 { get; set; }
        /// <summary>
        /// 赋值说明
        /// </summary>
        public string assignmentExplain { get; set; }
        /// <summary>
        /// 参与评论日期
        /// </summary>
        public string ParticipateDate { get; set; } 
    }
    /// <summary>
    /// 创建裁决所需的数据格式
    /// </summary>
    public class WxPostInvolveViewModel
    {
        public int id { get; set; }
        /// <summary>
        /// 原始赔偿
        /// </summary>
        public double orginalpay { get; set; }
        /// <summary>
        /// 案件id
        /// </summary>
        public int caseid { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 赔付方式
        /// </summary>
        public int modepay { get; set; }
        /// <summary>
        /// 赋值方式
        /// </summary>
        public int modedot { get; set; }
        /// <summary>
        /// 最满意的数额
        /// </summary>
        public double mostSatisfied { get; set; }
        /// <summary>
        /// 不赔偿的满意度
        /// </summary>
        public double nonSatisfied { get; set; }
        /// <summary>
        /// 两倍赔偿的满意度
        /// </summary>
        public double doublePay { get; set; }
        /// <summary>
        /// 额外赔偿方式1满意度
        /// </summary>
        public double otherSatisfied1 { get; set; }
        /// <summary>
        /// 额外赔偿方式2满意度
        /// </summary>
        public double otherSatisfied2 { get; set; }
        /// <summary>
        /// 额外赔偿方式3满意度
        /// </summary>
        public double otherSatisfied3 { get; set; }
        /// <summary>
        /// 赋值说明
        /// </summary>
        public string assignmentExplain { get; set; }
    }
}