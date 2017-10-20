using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.WholeSituation
{
    /// <summary>
    /// 案件状态常量
    /// </summary>
    class CaseStateClass
    {
        /// <summary>
        /// 草稿状态
        /// </summary>
        public const int DRAFT = 0;
        /// <summary>
        /// 审核状态
        /// </summary>
        public const int PENDING = 1;
        /// <summary>
        /// 发布状态
        /// </summary>
        public const int PUBLISH = 2;
        /// <summary>
        /// 完成状态
        /// </summary>
        public const int FINISH = 3;
        /// <summary>
        /// 申诉状态
        /// </summary>
        public const int COMPLAIN = 4;
        /// <summary>
        /// 全部案件
        /// </summary>
        public const int ALL = 5;
    }
    public enum CaseState
    {
        /// <summary>
        /// 草稿状态
        /// </summary>
        DRAFT,
        /// <summary>
        /// 审核状态
        /// </summary>
        PENDING,
        /// <summary>
        /// 发布状态
        /// </summary>
        PUBLISH,
        /// <summary>
        /// 完成状态
        /// </summary>
        FINISH,
        /// <summary>
        /// 申诉状态
        /// </summary>
        COMPLAIN,
        /// <summary>
        /// 全部案件
        /// </summary>
        ALL,
    }
    public enum AppealState
    {
        /// <summary>
        /// 未开始3
        /// </summary>
        UnFinish,
        /// <summary>
        /// 应诉人第一轮申诉
        /// </summary>
        RespondentToOne,
        /// <summary>
        /// 投诉人第一轮申诉
        /// </summary>
        AccuserOne,
        /// <summary>
        /// 应诉人第二轮申诉
        /// </summary>
        RespondentToTwo,
        /// <summary>
        /// 投诉人第二轮申诉
        /// </summary>
        AccuserTwo,
        /// <summary>
        /// 申诉结束
        /// </summary>
        Finish,

    }
    /// <summary>
    /// 申诉阶段的四个状态
    /// </summary>
    class AppealStateClass
    {
        public const int RESPONDENTONE = 1;
        public const int ACCUSORONE = 2;
        public const int RESPONDENTTWO = 3;
        public const int ACCUSORTWP = 4;

    }
    public class WX_Login
    {
        public static readonly string LogAppid = "wx696d1e9405459f86";
        public static readonly string LogSecret = "1f90c12741dacdd1c1bf3ece3e4587dd";
        public static readonly string Returl_Uri = "https%3a%2f%2fwww.jianglichina.com%2fwechat%2fcallback";
    }
}
