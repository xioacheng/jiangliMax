using DataDeal.Models;
using DataDeal.WholeSituation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Repository
{
    public class GetPoint
    {
        /// <summary>
        /// 获取单个人的赋值曲线点
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="caseid">案件id</param>
        /// <returns></returns>
        public string GetSinglePoint(string username,int caseid)
        {
            /*获取用户对案件的裁决信息*/
            VerdictInfo dbVerdivt = new VerdictInfo();
            var verdict = dbVerdivt.GetVerdivt(username,caseid).FirstOrDefault();

            WebClient wb = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            /*获取裁决信息的点*/
            var list = GetStandardPoints(verdict);
            /*通过集合点获取曲线信息*/
            postValues.Add("points", list.ToListString());
            //postValues.Add("points", "[[0,5],[100,10],[200,8]]");
            postValues.Add("curve_cnt", "21");
            postValues.Add("apikey", GlobalConfig.RequireApiKEY);
            byte[] byRemoteInfo = wb.UploadValues("http://api.gping.cn/assignment/", "POST", postValues);
            string sRemouteInfo = Encoding.Default.GetString(byRemoteInfo);
            return sRemouteInfo;
        }
        /// <summary>
        /// 通过裁决信息获取曲线点集合
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetSinglePoint(M_Involve item)
        {
            WebClient wb = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            /*获取裁决信息的点*/
            var list = GetStandardPoints(item);
            /*通过集合点获取曲线信息*/
            postValues.Add("points", list.ToListString());
            //postValues.Add("points", "[[0,5],[100,10],[200,8]]");
            postValues.Add("curve_cnt", "21");
            postValues.Add("apikey", GlobalConfig.RequireApiKEY);
            byte[] byRemoteInfo = wb.UploadValues("http://api.gping.cn/assignment/", "POST", postValues);
            string sRemouteInfo = Encoding.Default.GetString(byRemoteInfo);
            return sRemouteInfo;
        }
        /// <summary>
        /// 通过裁决信息获取标准点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<Point> GetStandardPoints(M_Involve item)
        {
            if (item == null)
            {
                /* 异常：未找到该裁决 */
                return null;
            }
            /* 异常：该裁决方式为单点赋值，则不应有裁决曲线 */
            List<Point> list = new List<Point>(){
                new Point{
                    x = item.MostSatisfied,
                    y = 10,
                },
                new Point{
                    x = item.MostSatisfied * 2,
                    y = item.DoublePay,
                },
                new Point{
                    x = 0,
                    y = item.NonSatisfied,
                },
            };
            return list;
        }
    }
    public class Point
    {
        /// <summary>
        /// 赔付金额
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// 满意度
        /// </summary>
        public double y { get; set; }
        public override string ToString()
        {
            return "[" + x + "," + y + "]";
        }
    }
    public static class PointHelper
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToListString(this List<Point> list)
        {
            StringBuilder result = new StringBuilder("[");
            foreach (var item in list)
            {
                result.Append(item.ToString());
                result.Append(",");
            }
            result.Remove(result.Length - 1, 1);
            result.Append("]");
            return result.ToString();
        }
    }
}
