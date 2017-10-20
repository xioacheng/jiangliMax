using DataDeal.WholeSituation;
using System;

namespace DataDeal.Models
{
    //案件列表
    public class M_Case
    {
        /// <summary>
        /// 案件ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 赔付金额
        /// </summary>
        public double originalpay { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// 投诉人
        /// </summary>
        public string Complainant { get; set; }
        /// <summary>
        /// 投诉人id
        /// </summary>
        public string ComplainantId { get; set; }
        /// <summary>
        /// 应诉人
        /// </summary>
        public string Respondent { get; set; }
        /// <summary>
        /// 用户输入时，应诉人电话
        /// </summary>
        public string RespondentPhone { get; set;}
        /// <summary>
        /// 用户输入时，应诉人邮箱
        /// </summary>
        public string RespondentEmail { get; set; }
        /// <summary>
        /// 用户输入时，应诉人地址
        /// </summary>
        public string RespondentAddress { get; set; }
        /// <summary>
        /// 应诉人id
        /// </summary>
        public string RespondentId { get; set; }
        /// <summary>
        /// 额外赔偿方式1
        /// </summary>
        public string OtherPay1 { get; set;}
        public double OtherPay1Num { get; set; }
        /// <summary>
        /// 额外赔偿方式2
        /// </summary>
        public string OtherPay2 { get; set; }
        public double OtherPay2Num { get; set; }
        /// <summary>
        /// 额外赔偿方式3
        /// </summary>
        public string OtherPay3 { get; set; }
        public double OtherPay3Num { get; set; }
        /// <summary>
        /// 赔偿方式
        /// </summary>
        public int ModeOfPay { get; set; }
        /// <summary>
        /// 案件陈述
        /// </summary>
        public string StatementOfCase { get; set; }
        /// <summary>
        /// 照片路径
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 视频路径
        /// </summary>
        public string Video { get; set; }
        /// <summary>
        /// 发布者ID
        /// </summary>
        public string PublisherId { get; set; }
        /// <summary>
        /// 草稿发布日期
        /// </summary>
        public DateTime DraftBeginDate { get; set; }
        /// <summary>
        /// 案件提交日期
        /// </summary>
        public DateTime CaseOfSubmit { get; set; }
        /// <summary>
        /// 案件发布日期
        /// </summary>
        public DateTime DateOfBegin { get; set; }
        /// <summary>
        /// 案件截至日期
        /// </summary>
        public DateTime DateOfEnd { get; set; }
        /// <summary>
        /// 案件伪删除
        /// </summary>
        public int DeleteRecycle { get; set; }
        /// <summary>
        /// 案件发布状态
        /// </summary>
        public CaseState StateOfCase { get; set; }
        /// <summary>
        /// 案件整体曲线点
        /// </summary>
        public string PointOfCase { get; set; }
        /// <summary>
        /// 投诉人的第一轮申诉
        /// </summary>
        public string TrunOneComplainByComplainant { get; set; }
        /// <summary>
        /// 应诉人的第一轮申诉
        /// </summary>
        public string TrunOneComplainByRespondent { get; set; }
        /// <summary>
        /// 投诉人的第二轮申诉
        /// </summary>
        public string TrunTwoComplainByComplainant { get; set; }
        /// <summary>
        /// 应诉人的第二轮申诉
        /// </summary>
        public string TrunTwoComplainByRespondent { get; set; }
        /// <summary>
        /// 申诉阶段的区分标记
        ///     1:应诉人的第一轮申诉
        ///     2:投诉人的第一轮申诉
        ///     3:应诉人的第二轮申诉
        ///     4:投诉人的第二轮申诉
        /// </summary>
        public AppealState Condition { get; set; }
        
    }
}
