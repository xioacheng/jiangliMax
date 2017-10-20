using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models
{
    //审查员调解
    public class M_ExaminerConte
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 案件的ID
        /// </summary>
        public int CaseID { get; set; }
        /// <summary>
        /// 审查员的昵称
        /// </summary>
        public string Examiner { get; set; }
        /// <summary>
        /// 审查员的ID
        /// </summary>
        public string ExaminerID { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 审查日期
        /// </summary>
        public DateTime ExamineDate { get; set; }
    }
}
