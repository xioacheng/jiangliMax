using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models
{
    //用户列表属性
    public class M_JiangliUser:IdentityUser
    {
        //角色
        public string RoleName { get; set; }
        //登录标识码，登录后生成的唯一标识码
        public string token { get; set; }
        //昵称
        public string nickName { get; set; }
        //权重
        public double Weight { get; set; }
        //发布的案件数量
        public int NumberOfCase { get; set; }
        //权重变化曲线
        public int PointOfWeigth { get; set; }
        //放到回收站 0 回收站中
        public int DeleteRecycle { get; set; }
        //微信openid
        public string openid { get; set; }
        //微信unionid
        public string unionid { get; set; }
        //身份证
        public string IDcard { get; set; }
        //性别 0 男 1女
        public int Gender { get; set; }
        //爱好
        public string Hobby { get; set; }
        //血型
        public string BloodType { get; set; }
        //注册日期
        public DateTime RegisterDate { get; set; }
        //最后登录日期
        public DateTime DateOfLast { get; set; }
        //简短介绍
        public string SelfIntroduction { get; set; }
        //头像地址
        public string Avatar_url { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string profession { get; set; }
        /// <summary>
        /// 出生年
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 工作id号
        /// </summary>
        public int job_id { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        public string qq { get; set; }
        public string mobile { get; set; }
        /// <summary>
        /// 主页
        /// </summary>
        public string homepage { get; set; }
    }
}
