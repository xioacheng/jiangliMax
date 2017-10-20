using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDeal.Models.WxModels.Case
{
    /// <summary>
    /// 发送给微信的评论格式
    /// </summary>
    public class WxCommentViewModel
    {
        public int id { get; set;  }
        public int caseid { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string uid { get; set; }
        public string username { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 被回复的用户id
        /// </summary>
        public string pid { get; set; }
        public string pusername { get; set; }
        /// <summary>
        /// 日期--时间
        /// </summary>
        public string created_at { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar_url { get; set; }

    }
}