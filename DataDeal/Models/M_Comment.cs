using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models
{
    public class M_Comment
    {
        public int ID { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int CaseId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 评论者姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 评论日期
        /// </summary>
        public DateTime Date { get; set; }
    }
}
