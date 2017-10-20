using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models
{
    //权限类
    public class M_Permit
    {
        /// <summary>
        /// 唯一标识ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 控制器的名字
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// action名字
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermitName { get; set; }
        /// <summary>
        /// url地址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 创建者id
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 创建者用户name
        /// </summary>
        public string CreateByName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateOn { get; set; }
        /// <summary>
        /// 修改者id
        /// </summary>
        public int ModifyBy { get; set; }
        /// <summary>
        /// 修改者用户Name
        /// </summary>
        public string ModifyByName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyOn { get; set; }
    }
}
