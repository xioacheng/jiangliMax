using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models.WxModels
{
    public class BaseViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string avatar_url { get; set; }
        public string RoleName { get; set; }
    }
}
