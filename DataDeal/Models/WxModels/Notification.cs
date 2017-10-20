using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models.WxModels
{
    /// <summary>
    /// 通知模型类
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// 唯一id标识
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 通知消息内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 通知发起人id
        /// </summary>
        public int speakerid { get; set; }
        /// <summary>
        /// 接收人id
        /// </summary>
        public int receiverid { get; set; }

        /// <summary>
        /// 通知创建日期
        /// </summary>
        public DateTime create_at { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public int hasRead { get; set; }
    }
}
