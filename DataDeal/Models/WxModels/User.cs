using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models.WxModels
{
    /// <summary>
    /// 微信登陆的用户基本信息
    /// </summary>
    public class WxRegisterViewModel
    {
        public string openid { get; set; }
        public string unionid { get; set; }
        public string nickName { get; set; }
        public int gender { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public string account { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

    }
    /// <summary>
    /// 微信登陆用户的返回信息
    /// </summary>
    public class WxResultLoginViewModel
    {
        public bool succeed { get; set; }
        public string openid { get; set; }
        public string unionid { get; set; }
        public string userid { get; set; }

        /// <summary>
        /// 角色--
        /// </summary>
        public int role { get; set; }
        public string token { get; set; }
        /// <summary>
        /// 0 用户名不符合规范 1用户名重复
        /// </summary>
        public int reason { get; set; }

    }
}
