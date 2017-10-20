using DataDeal.WholeSituation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataDeal.Models.WxModels.Case
{
    /// <summary>
    /// 获取全部的案件列表项
    ///     案件的基本信息
    ///     案件发起者的基本信息
    /// </summary>
    public class WxCaseListItemViewModel
    {
        /// <summary>
        /// 案件的基本信息
        /// </summary>
        public BasicCaseViewModel basic { get; set; }
        
        /// <summary>
        /// 案件的发起人的基本信息
        /// </summary>
        public BasicUserViewModel author { get; set; }

    }
   
    /// <summary>
    /// 微信用户所请求的案件详细信息
    ///     案件的基本信息
    ///     案件所包含的裁决列表
    /// </summary>
    public class WxCaseDetailViewModel
    {
        /// <summary>
        /// 案件的发起者
        /// </summary>
        public BasicUserViewModel user { get; set; }
        /// <summary>
        /// 案件的基本信息
        /// </summary>
        public BasicCaseViewModel basic { get; set; }

        /// <summary>
        /// 该案件下的的所有评论
        /// </summary>
        public IEnumerable<WxInvolveViewModel> involves { get; set; }
        /// <summary>
        /// 应诉人
        /// </summary>
        public BasicUserViewModel respondent { get; set; }
        /// <summary>
        /// 案件发起人索要赔偿
        /// </summary>
        public double orginalpay { get; set; }
        /// <summary>
        /// 投诉人第一轮申诉
        /// </summary>
        public string userturnone { get; set; }
        /// <summary>
        /// 应诉人第一轮申诉
        /// </summary>
        public string respondentturnone { get; set;  }
        /// <summary>
        /// 投诉人第二轮申诉
        /// </summary>
        public string userturntwo { get; set; }
        /// <summary>
        /// 应诉人第二轮申诉
        /// </summary>
        public string respondentturntwo { get; set; }
        /// <summary>
        /// 申诉阶段的标记
        /// </summary>
        public AppealState condition { get; set; }
        /// <summary>
        /// 举证照片的url
        /// </summary>
        public string imageSrc { get; set;}

        
    }

}