using DataDeal.Models.RepositoryModal;
using DataDeal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jiangliMax.Controllers
{
    public class WeChatController : Controller
    {
        public ActionResult Group()
        {
            GetGroupPoint db = new GetGroupPoint();
            db.GroupPoint(2);
            return View();
        }
        public ActionResult CallBack(string code, string state)
        {
            //code = "061J5Th519beZN1VuIj51OLPh51J5Thm";
            //state = "STATE";
            UserInfo dbuser = new UserInfo();
            var result = dbuser.userLogin(code, state);
            //获取微信用户信息失败
            if (result.result == 0)
            {
                return Redirect("/home");
            }
            //用户未注册
            else if (result.result == 1)
            {
                //进入填写信息界面
                return View(result);
            }
            //用户已经注册直接登录
            else if (result.result == 2)
            {
                //标识2已经登录成功 进入主题页面
                return Redirect("/home"); ;
            }
            //其他登录标识
            else
            {
                return Redirect("/home");
            }
        }
        /// <summary>
        /// 微信登录二维码显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string url = "https://open.weixin.qq.com/connect/qrconnect?appid="
                + GlobalVar.LogAppid + "&redirect_uri=" + GlobalVar.Returl_Uri
                + "&response_type=code&scope=snsapi_login&state=STATE#wechat_redirect";
            return Redirect(url);
        }
        public ActionResult LogOut()
        {
            UserInfo dbuser = new UserInfo();
            dbuser.Logout();
            return Redirect("/home");
        }
        public ActionResult Register(WeChatRegister model)
        {

            WeChatResult viewModel = new WeChatResult
            {
                userinfo = new Wx_login_userinfo
                {
                    openid = model.openid,
                    nickname = model.nickname,
                    sex = model.sex,
                    language = model.language,
                    country = model.country,
                    province =model.province,
                    headimgurl = model.headimgurl,
                    unionid = model.unionid,
                },
                result = 1
            };
            //ModelState.AddModelError("", "用户名不存在");//错误显示
            UserInfo dbsuer = new UserInfo();
            var create = dbsuer.WxRegister(model);
            if (create.result == 0||create.result==-1)
            {
                //0重复  -1用户名不能包含汉字
                string str = "";
                if(create.resultEmail == 0 || create.resultName == 0 || create.resultPhone == 0)
                {
                    if (create.resultEmail == 0) str += "邮箱，";
                    if (create.resultName == 0) str += "用户名,";
                    if (create.resultPhone == 0) str += "电话,";
                    ModelState.AddModelError("", str + "已存在，请重新填写");
                    return View("CallBack", viewModel);
                }
                else
                {
                    ModelState.AddModelError("",  "用户名只能包含字母和数字或下划线");
                    return View("CallBack", viewModel);
                }
            }
            else
            {
                //用户登录
                var logresult = dbsuer.Login(create.userinfo);
                if (logresult == 1)
                {
                    return Redirect("/home");
                }
                else
                {
                    ModelState.AddModelError("", "服务器原因注册失败");
                    return View();
                }
            }
        }
    }
}