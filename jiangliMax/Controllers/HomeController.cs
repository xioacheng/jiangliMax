using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataDeal.Repository;
using DataDeal.Models.RepositoryModal;
using DataDeal.Models;

namespace jiangliMax.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页话题
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //判断用户是都已经登录
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //登陆后返回给用户相应的信息
                CaseInfoList dbUserCase = new CaseInfoList();
                var usercase = dbUserCase.GetUserCase(HttpContext.User.Identity.Name);
                ViewBag.UserCase = usercase;
            }
            //公共信息
            CaseInfoList dbcase = new CaseInfoList();
            var caselist = dbcase.HomeCaseList();
            //var temp = caselist.FirstOrDefault().commentlist.Count();
            return View(caselist);
        }
        public ActionResult searchCase(string searchTxt)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //登陆后返回给用户相应的信息
                CaseInfoList dbUserCase = new CaseInfoList();
                var usercase = dbUserCase.GetUserCase(HttpContext.User.Identity.Name);
                ViewBag.UserCase = usercase;
            }
            CaseInfoList dbcase = new CaseInfoList();
            var caselist = dbcase.HomeCaseList().Where(t=>t.casetitle.Contains(searchTxt)||t.casecontent.Contains(searchTxt)).ToList();
            return View("Index",caselist);
        }
        /// <summary>
        /// 发文  案件发布
        /// </summary>
        /// <returns></returns>
        public ActionResult publish()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("Index");
            }
            return View();
        }
        public ActionResult draft(int caseid,string username)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("publish");
            }
            if (User.Identity.Name != username)
            {
                return View("publish");
            }
            CaseInfo dbcase = new CaseInfo();
            var draft = dbcase.GetCaseById(caseid);
            ViewBag.draft = draft;
            return View("publish");
        }
        /// <summary>
        /// 案件发布
        /// </summary>
        /// <param name="modal"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult publish(casepublist modal)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/home");
            }
            //判断是否存在respondid 存在的话是站内输入  不存在的话是手动输入
            //if (modal.respondentid == null)
            //{
            //    if (modal.respondent == null) { }
            //    ModelState.AddModelError("","应诉人不能为空");
            //    return View();
            //}

            //应诉人可以为空
            if (string.IsNullOrEmpty(modal.content))
            {
                ModelState.AddModelError("", "案件内容不能为空");
                return View();
            }
            if (modal.content.Length < 50)
            {
                ModelState.AddModelError("", "输入的案件信息需要在50字以上，请补充内容");
                return View();
            }
            CaseInfo dbcase = new CaseInfo();
            var result = dbcase.PublishCase(modal);
            if (result == 1)
            {
                return Redirect("/home");
            }
            ModelState.AddModelError("","发布案件出现问题，请修改信息后重新发布");
            return View();
        }
        /// <summary>
        /// 删除草稿模板
        /// </summary>
        /// <param name="caseid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public ActionResult deletedraft(int caseid,string username)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("index");
            }
            UserInfo user = new UserInfo();
            string username2 = User.Identity.Name;
            string userid = user.GetUseByName(username2).Id;
            if (username == username2)
            {
                CaseInfo dbcase = new CaseInfo();
                var result = dbcase.DeleteCaseById(caseid);
                if (result == 1)
                {
                    return Redirect("/home/person?userid=" + userid);
                }
            }
            return View("index");
        }
        /// <summary>
        /// 案件详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult article(int ? id)
        {
            int caseid = id ?? 0;
            if (caseid <= 0)
            {
                return Redirect("/home/index");
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    VerdictInfo dbverdict = new VerdictInfo();
                    var temp = dbverdict.GetVerdivt(HttpContext.User.Identity.Name, caseid);
                    var temp2 = temp.Count();
                    ViewBag.verdict = temp.FirstOrDefault();
                }
                CaseInfo dbcase = new CaseInfo();
                var caseitem = dbcase.PublicCase(caseid);
                return View(caseitem);
            }
        }
        /// <summary>
        /// 案件裁决界面
        /// </summary>
        /// <returns></returns>
        public ActionResult verdict(int ? id)
        {
            int caseid = id ?? 0;
            if (!User.Identity.IsAuthenticated||caseid==0)
            {
                return Redirect("/home");
            }
            CaseInfo dbcase = new CaseInfo();
            ViewBag.Case = dbcase.GetCaseById(caseid) ;
            return View();
        }
        [HttpPost]
        public ActionResult verdict(string pctype,Verdict model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/home");
            }
            string username = HttpContext.User.Identity.Name;
            //pctype 1为必须赔偿 2为互不赔偿
            model.styleOfpay = pctype;
            VerdictInfo dbverdict = new VerdictInfo();
            int result = dbverdict.AddVerdict(username,model);
            if (result == 1)
            {
                return Redirect("/home/article?id="+model.caseid);
            }
            else
            {
                return Redirect("/home");
            }
        }
        /// <summary>
        /// 个人主页
        /// </summary>
        /// <returns></returns>
        public ActionResult person(string  userid)
        {
            if (userid == null)
            {
                return Redirect("/home");
            }
            //用户自己的案件的话获取用户的其他私人案件
            if (User.Identity.IsAuthenticated)
            {
                UserInfo dbinfo = new UserInfo();
                CaseInfoList dbcase = new CaseInfoList();
                var userLog = dbinfo.GetUserById(userid);
                if (userLog != null)
                {
                    if (User.Identity.Name == userLog.UserName)
                    {
                        var caselist = dbcase.GetUserCase(userLog.UserName);
                        ViewBag.UserCase = caselist;
                    }
                }
            }
            UserInfo dbuser = new UserInfo();
            var user = dbuser.userPage(userid);
            return View(user);
        }
        /// <summary>
        /// 个人信息修改
        /// </summary>
        /// <returns></returns>
        public ActionResult personinfo(string  userid)
        {
            if (!User.Identity.IsAuthenticated||userid==null)
            {
                return Redirect("/home");
            }
            UserInfo dbuser = new UserInfo();
            var userinfo = dbuser.GetUserById(userid);
            return View(userinfo);
        }
        [HttpPost]
        public ActionResult personinfo(M_JiangliUser model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/home");
            }
            string username = User.Identity.Name;
            UserInfo dbuser = new UserInfo();
            var user = dbuser.GetUseByName(username);
            if (model.address != null)
                user.address = model.address;
            if (model.PhoneNumber != null)
                user.PhoneNumber = model.PhoneNumber;
            if (model.birthday != null)
                user.birthday = model.birthday;
            if (model.qq != null)
                user.qq = model.qq;
            if (model.BloodType != null)
                user.BloodType = model.BloodType;
            if (model.Hobby != null)
                user.Hobby = model.Hobby;
            if (model.SelfIntroduction != null)
                user.SelfIntroduction = model.SelfIntroduction;
            if (model.profession != null)
                user.profession = model.profession;
            int result = dbuser.UpDateUser(user);
            if (result == 1)
            {
                ModelState.AddModelError("", "修改成功");
                return View(user);
            }
            else
            {
                ModelState.AddModelError("","修改失败");
                return View(user);
            }
        }
        /// <summary>
        /// 网站首页的内容展示
        /// </summary>
        /// <returns></returns>
        public ActionResult page()
        {
            return View();
        }
    }
}