using DataDeal.BLL;
using DataDeal.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;

namespace DataDeal.Repository
{
    public class GetGroupPoint
    {
        /*
           [
               {"user_id": "38883de9595e447a8f5ab016d63cd404","points": [["0.00", "2.00"], ["5000.0000", "10.00"], ["6000.0", "7.00"]],  "weight": 100.0} ,
               {"user_id": "92fb283cfabc4c3db5990814cc01ac5e","points": [["0.00", "3.00"], ["3000.0000", "10.00"], ["6000.0", "8.00"]],  "weight": 100.0} ,
           ]
       */
        public int GroupPoint(int caseid)
        {
            InvolveService dbinvolve = new InvolveService();
            //获取参与案件的所有裁决
            var verdictList = dbinvolve.LoadEntities(t=>t.CaseId==caseid);
            if (verdictList != null)
            {
                string str = StandardPoint(verdictList);
                //str = "[{ \"user_id\": \"38883de9595e447a8f5ab016d63cd404\", \"points\": [[\"0.00\", \"2.00\"], [\"5000.0000\", \"10.00\"], [\"6000.0\", \"7.00\"]], \"weight\": 100.0} ,{\"user_id\": \"92fb283cfabc4c3db5990814cc01ac5e\",\"points\": [[\"0.00\", \"3.00\"], [\"3000.0000\", \"10.00\"], [\"6000.0\", \"8.00\"]],  \"weight\": 100.0} ]";
                WebClient wb = new WebClient();
                NameValueCollection PostVars = new NameValueCollection();
                PostVars.Add("assignments", str);
                PostVars.Add("apikey", "51820bc0392111e6993d40167e6636e6");
                byte[] byRemoteInfo = wb.UploadValues("http://api.gping.cn/case/1/adjudicates/", "POST", PostVars);
                string sRemoteInfo = Encoding.Default.GetString(byRemoteInfo);
                GpApi api = new GpApi();
                GpApi api2  = (GpApi)JsonConvert.DeserializeObject(sRemoteInfo , api.GetType());
            }
            return 1;
        }
        /// <summary>
        /// 获取标准点
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string StandardPoint(IQueryable<M_Involve> list)
        {
            UserInfo dbuser = new UserInfo();
            string cmd = null;
            foreach(var item in list)
            {
                //找用户
                var user = dbuser.GetUserById(item.UserID);
                //必须赔偿
                if(item.ModeOfPay == 1)
                {
                    string userid = user.Id.Replace("-","");;
                   string str = "{ \"user_id\": \""+userid+"\", \"points\": [[\"0.00\", \""+item.NonSatisfied+"\"], [\""
                        +item.MostSatisfied+"\", \"10.00\"], [\""+item.MostSatisfied*2+"\", \""+item.DoublePay+"\"]], \"weight\": "+user.Weight+"} ,";
                    cmd += str;
                }
                //不用赔偿 互不赔偿时候的满意度为10不在群体满意度的计算范围内
            }
            cmd = cmd.Substring(0, cmd.Length - 1);
            cmd = "[" + cmd + "]";
            return cmd;
        }
    }
    public  class GpApi
    {
        public ApiData data { get; set; }
        public string success { get; set; }
    }
    public class ApiData
    {
        public object curve { get; set; }
        public object weight { get; set; }
    }
}
