using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models.RepositoryModal
{
    public class Wx_login_Result
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }

        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
        public string unionid { get; set; }
    }
    public class Wx_login_userinfo
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string language { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string headimgurl { get; set; }
        public string unionid { get; set; }
    }
    public class WeChatResult
    {
        public Wx_login_userinfo userinfo { get; set; }
        /// <summary>
        /// 微信请求返回用户信息结果标识0失败 1未注册 2已注册
        /// </summary>
        public int result { get; set; }
    }
    public class WeChatRegister : Wx_login_userinfo
    {
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
    public class WXRegResult
    {
        /// <summary>
        /// 0 重复 1成功
        /// </summary>
        public int resultName { get; set; }
        /// <summary>
        /// 0 重复 1成功
        /// </summary>
        public int resultEmail { get; set; }
        /// <summary>
        /// 0 重复 1成功
        /// </summary>
        public int resultPhone { get; set; }
        /// <summary>
        /// 0重复 1成功 -1创建失败
        /// </summary>
        public int result { get; set; }
        /// <summary>
        /// 返回用户基本信息
        /// </summary>
        public M_JiangliUser userinfo { get; set; }
    }
}
