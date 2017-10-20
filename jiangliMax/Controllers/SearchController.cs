using DataDeal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace jiangliMax.Controllers
{
    public class SearchController : Controller
    {
        public string GetPoint(int caseid)
        {
            string username = User.Identity.Name;
            GetPoint dbpoint = new GetPoint();
            string result = dbpoint.GetSinglePoint(username, caseid);
            return result;
        }
        public ActionResult addFollow(int type,int caseid)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/home");
            }
            //type 0 取消关注 1添加关注
            Follow dbfollow = new Follow();
            if (type == 1)
            {
                int data = dbfollow.addFollow(User.Identity.Name,caseid);
                if (data >= 0)
                    return Json(new { num = data, result = true });
                else
                    return Json(new { num = data, result = false });
            }
            if(type == 0)
            {
                int data = dbfollow.removeFollow(User.Identity.Name, caseid);
                if (data >= 0)
                    return Json(new { num = data, result = true });
                else
                    return Json(new { num = data, result = false });
            }
            else
            {
                return Redirect("/home");
            }
            
        }
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="content"></param>
        /// <param name="caseid"></param>
        /// <returns></returns>
        public ActionResult Discuss(string content,string caseid)
        {
            if (!User.Identity.IsAuthenticated)
                return Json("-1");
            string username = User.Identity.Name;
            Comment dbcomment = new Comment();
            var result = dbcomment.AddDiscuss(username,content,int.Parse(caseid));
            if (result == 1)
                return Json("true");
            else return Json("false");
        }
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="searchname"></param>
        /// <param name="searchphone"></param>
        /// <param name="searchemail"></param>
        /// <returns></returns>
        public ActionResult UserList(string searchname,string searchphone,string searchemail)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }
            if (searchname == "" && searchphone == ""&& searchemail == "")
            {
                return null;
            }
            UserInfo dbuser = new UserInfo();
            var userlist = dbuser.JQSearch(searchname,searchphone,searchemail);
            List<dynamic> result = new List<dynamic>();
            foreach(var item in userlist)
            {
                string phone = "";
                if (item.PhoneNumber != null && item.PhoneNumber != "")
                {
                    phone = Regex.Replace(item.PhoneNumber, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");
                }
                dynamic temp = new
                {
                    userid = item.Id,
                    username = item.UserName,
                    userphone = phone,
                    useremail = item.Email,
                };
                result.Add(temp);
            }
            return Json(result);
        }
    }
}