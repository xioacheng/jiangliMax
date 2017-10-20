using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models
{
    //参与案件裁决
    public class M_Involve
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 参与者Id
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 所属案件的ID -- 新增
        /// </summary>
        public int CaseId { get; set; }
        /// <summary>
        /// 赋值方式
        /// </summary>
        public int ModeOfDot { get; set; }
        /// <summary>
        /// 赔付方式 1 必须赔偿 2互不赔偿
        /// </summary>
        public int ModeOfPay { get; set; }
        /// <summary>
        /// 最满意的数额
        /// </summary>
        public double MostSatisfied { get; set; }
        /// <summary>
        /// 不赔偿的满意度
        /// </summary>
        public double NonSatisfied { get; set; }
        /// <summary>
        /// 两倍赔偿的满意度
        /// </summary>
        public double DoublePay { get; set; }
        /// <summary>
        /// 赋值说明
        /// </summary>
        public string AssignmentExplain { get; set; }
        /// <summary>
        /// 参与评论日期
        /// </summary>
        public DateTime ParticipateDate { get; set; }
        /// <summary>
        /// 修改裁决日期
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// 裁决生成的点集合
        /// </summary>
        public string PointList { get; set; }
        public double OtherNum1 { get; set; }
        public double OtherNum2 { get; set; }
        public double OtherNum3 { get; set; }
    }
}
