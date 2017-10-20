using DataDeal.BLL;
using DataDeal.Infrastructure;
using DataDeal.Models;
using DataDeal.Models.RepositoryModal;
using DataDeal.Models.WxModels;
using DataDeal.Repository.WxRepository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;

namespace DataDeal.Repository
{
    public class UserInfo
    {
        private static UserInfo user = new UserInfo();
        public static UserInfo Current { get { return user; } }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpDateUser(M_JiangliUser user)
        {
            var result = UserManager.Update(user);
            if (result.Succeeded)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 通过用户id和token查找用户是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool findToken(string id,string token)
        {
            var result = UserManager.Users.Where(t => t.Id == id && t.token == token);
            if (result == null)
            {
                return false;
            }
            if (result.Count() == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ajax查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public IQueryable<M_JiangliUser> JQSearch(string name,string phone,string email)
        {
            try
            {
                var userlist = UserManager.Users.Where(t => t.UserName.Contains(name) || t.nickName.Contains(name) && t.PhoneNumber.Contains(phone) && t.Email.Contains(email));
                return userlist;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
        /// <summary>
        /// 通过用户id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public M_JiangliUser GetUserById(string id)
        {
            var user = UserManager.FindById(id);
            return user;
        }
        public BasicUserViewModel GetBasicUserById(string userId)
        {
            CaseService dbcase = new CaseService();
            M_JiangliUser user = UserManager.Users.Where(t => t.Id == userId).FirstOrDefault();
            if (user == null) return null;
            return new BasicUserViewModel()
            {
                id = user.Id,
                nickname = user.nickName,
                avatar_url = user.Avatar_url,
                numbercase = dbcase.LoadEntities(t=>t.PublisherId==user.Id),
                weight = user.Weight,
            };
        }

        public M_JiangliUser GetUseByName(string name)
        {
            if(name==null) return null;
            var user = UserManager.FindByName(name);
            return user;
        }
        /// <summary>
        /// 返回个人主页数据
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public HomePerson userPage(string userid)
        {
            CaseService dbcase = new CaseService();
            FollowCaseService dbfollow = new FollowCaseService();
            InvolveService dbinvolve = new InvolveService();
            M_JiangliUser user = GetUserById(userid);
            if (userid == null)
            {
                return null;
            }
            //关注
            var follow = dbfollow.LoadEntities(t=>t.userid==userid);
            //参与
            var involve = dbinvolve.LoadEntities(t=>t.UserID==userid);
            //已发布
            var publishlist = dbcase.LoadEntities(t => t.StateOfCase == WholeSituation.CaseState.PUBLISH&&t.PublisherId==userid);
            //已完成
            var finishlist = dbcase.LoadEntities(t => t.StateOfCase == WholeSituation.CaseState.FINISH && t.PublisherId == userid);
            //参与的案件列表
            List<IQueryable<M_Case>> participatelist = new List<IQueryable<M_Case>>();
            //关注的案件列表
            List<IQueryable<M_Case>> followlist = new List<IQueryable<M_Case>>();
            //所有已关注案件
            if (follow.Count() != 0)
            {
                foreach(var followitem in follow)
                {
                    IQueryable<M_Case> caseitem = dbcase.LoadEntities(t=>t.ID==followitem.caseid);
                    followlist.Add(caseitem);
                }
                
            }
            //所有参与案件
            if (involve.Count() != 0)
            {
                foreach (var involveitem in involve)
                {
                    IQueryable<M_Case> caseitem = dbcase.LoadEntities(t => t.ID == involveitem.CaseId);
                    participatelist.Add(caseitem);
                }
            }
            return new HomePerson
            {
                userid = userid,
                username = user.UserName,
                hobby = user.Hobby,
                headimg = user.Avatar_url,
                weight = user.Weight,
                publishlist = publishlist,
                finishlist = finishlist,
                followlist = followlist,
                participatelist = participatelist,
            };
        }
        /// <summary>
        /// 测试使用
        /// </summary>
        /// <returns></returns>
        public M_JiangliUser AddUser()
        {
            UserManager.Create(new M_JiangliUser { UserName="haibin2",});
            return UserManager.FindByName("haibin2");
        }
        /// <summary>
        /// 第一步：请求CODE
        /// 第二步：通过code获取access_token
        /// 第三步：通过access_token调用接口
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public WeChatResult userLogin(string code, string state)
        {

            WeChatResult WXresult = new WeChatResult();

            //直接登陆
            //LoginUnionid("o30T-wQKMCy-LiyR6qHxzKnjFFAI");
            //return new WeChatResult
            //{
            //    result = 2,
            //};
            //以上代码发布前需要注释

            WXresult.userinfo = new Wx_login_userinfo();
            //通过code 和 state获取微信用户的基本信息  
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid="
               + WholeSituation.WX_Login.LogAppid + "&secret="
               + WholeSituation.WX_Login.LogSecret + "&code="
               + code + "&grant_type=authorization_code";
            //微信信息获取 openid 
            try
            {
                WebClient wb = new WebClient();
                Stream sc = wb.OpenRead(url);
                StreamReader sr = new StreamReader(sc);
                string result = sr.ReadToEnd();
                Wx_login_Result li = JsonConvert.DeserializeObject<Wx_login_Result>(result);
                sc.Close(); sr.Close();
                //用户存在的话直接登陆
                if (li.unionid != null)
                {
                    if (UserManager.Users.Where(t => t.unionid == li.unionid).Count() > 0)
                    {
                        if (LoginUnionid(li.unionid) == 1)
                            WXresult.result = 2;
                        else WXresult.result = -1;
                        return WXresult;
                    }
                }
                else
                {
                    if (UserManager.Users.Where(t => t.openid == li.openid).Count() > 0)
                    {
                        if (Login(li.openid) == 1)
                            WXresult.result = 2;
                        else WXresult.result = -1;
                        return WXresult;
                    }
                }
                //获取验证失败 返回登陆失败标识码
                if (li.access_token == null)
                {
                    WXresult.result = 0;
                    return WXresult;
                }
                //通过openid 和access_token获取用户的基本信息
                string url2 = "https://api.weixin.qq.com/sns/userinfo?access_token=" + li.access_token + "&openid=" + li.openid + "&lang=zh-CN";
                try
                {
                    WebClient wb2 = new WebClient();
                    Stream sc2 = wb2.OpenRead(url2);
                    StreamReader sr2 = new StreamReader(sc2);
                    string result2 = sr2.ReadToEnd();
                    Wx_login_userinfo userinfo = JsonConvert.DeserializeObject<Wx_login_userinfo>(result2);
                    sc2.Close(); sr2.Close();
                    //返回用户信息和用户为注册标识
                    WXresult.result = 1;
                    WXresult.userinfo = userinfo;
                    return WXresult;
                }
                catch (Exception e)
                {
                    //记录登录失败日志
                    WXresult.result = 0;
                    return WXresult;
                }
            }
            catch (Exception e)
            {
                //记录登录失败日志
                WXresult.result = 0;
                return WXresult;
            }
        }
        /// <summary>
        /// 微信用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WXRegResult WxRegister(WeChatRegister model)
        {
            WXRegResult result = new WXRegResult();
            //用户名是否重复
            if (UserManager.Users.Where(t => t.UserName == model.username).Count()>0)
            {
                result.resultName = 0;
            }
            else
            {
                result.resultName = 1;
            }
            //邮箱是否重复
            if (UserManager.Users.Where(t => t.Email == model.email).Count() > 0)
            {
                if(model.email==null) result.resultEmail = 1;
                else result.resultEmail = 0;
            }
            else
            {
                result.resultEmail = 1;
            }
            //电话是否重复
            if (UserManager.Users.Where(t => t.PhoneNumber == model.phone).Count() > 0)
            {
                if (model.phone == null) result.resultPhone = 1;
                else result.resultPhone = 0;
            }
            else
            {
                result.resultPhone = 1;
            }
            //重复返回错误结果
            if (result.resultEmail == 0 || result.resultName == 0 || result.resultPhone == 0)
            {
                result.result = 0;
                return result;
            }
            //没有重复进行用户注册
            M_JiangliUser user = new M_JiangliUser()
            {
                UserName = model.username,
                Email = model.email,
                PhoneNumber = model.phone,
                openid = model.openid,
                unionid = model.unionid,
                Gender = int.Parse(model.sex),
                address = model.country == null ? "" : model.country + "-" + model.province == null ? "" : model.province,
                nickName = model.nickname,
                RegisterDate = DateTime.Now,
                Weight = 100,
                DateOfLast = DateTime.Now,
                Avatar_url = model.headimgurl,
            };
            var createResult = UserManager.Create(user);
            if (createResult.Succeeded)
            {
                var update = UserManager.Users.Where(t => t.UserName == model.username).FirstOrDefault();
                update.Avatar_url = model.headimgurl;
                var updateresult = UserManager.Update(update);
                result.userinfo = UserManager.Users.Where(t => t.UserName == model.username).FirstOrDefault();
                result.result = 1;
                return result;
            }
            else
            {
                result.result = -1;
                return result;
            }
        }
        #region 网页用户通过用户信息登录
        public int Login(M_JiangliUser user)
        {
            var search = UserManager.Users.Where(t=>t.Id==user.Id).FirstOrDefault();
            search.DateOfLast = DateTime.Now;
            var temp = UserManager.Update(search);
            if (search != null)
            {
                ClaimsIdentity ident = UserManager.CreateIdentity(user,
                               DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties{IsPersistent = false}, ident);
                return 1;
            }
            return 0;
        }
        #endregion
        #region 网页用户通过openid unionid登录
        public int Login(string openid)
        {
            var search = UserManager.Users.Where(t=>t.openid==openid).FirstOrDefault();
            search.DateOfLast = DateTime.Now;
            var temp = UserManager.Update(search);
            if (search != null)
            {
                ClaimsIdentity ident = UserManager.CreateIdentity(search,
                               DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                return 1;
            }
            return 0;
        }
        public int LoginUnionid(string unionid)
        {
            try
            {
                var search = UserManager.Users.Where(t => t.unionid == unionid).FirstOrDefault();
                search.DateOfLast = DateTime.Now;
                var temp = UserManager.Update(search);
                if (search != null)
                {
                    ClaimsIdentity ident = UserManager.CreateIdentity(search,
                                   DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        #endregion
        /// <summary>
        /// 登录注销
        /// </summary>
        public void Logout()
        {
            AuthManager.SignOut();
        }
        #region 微信客户端响应操作
        /// <summary>f
        /// 通过昵称获取相关用户列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public IEnumerable<BasicUserViewModel> GetUsersByNickname(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return null;
            }
            var userinfo = UserManager.Users.Where(t => t.nickName.Contains(q) || t.UserName.Contains(q));
            List<BasicUserViewModel> result = new List<BasicUserViewModel>();
            foreach (var item in userinfo)
            {
                result.Add(new BasicUserViewModel()
                {
                    avatar_url = item.Avatar_url,
                    id = item.Id,
                    nickname = item.nickName,
                    numbercase = CaseList.Current.m_getCasesByUserid(item.Id),
                    weight = item.Weight,
                });
            }
            return result;
        }
        public WxResultLoginViewModel Login(string token,string openid,string unionid)
        {
            //var user = UserManager.Users.Where(t => t.openid == openid).FirstOrDefault();
            var userlist = UserManager.Users.Where(t => t.unionid == unionid);
            if (userlist.Count() == 0)
            {
                return new WxResultLoginViewModel
                {
                    succeed = false,
                    openid = openid,
                    unionid = unionid,
                };
            }
            var user = UserManager.Users.Where(t=>t.unionid==unionid).FirstOrDefault();
            if (user != null)
            {
                user.DateOfLast = DateTime.Now;
                user.token = token;
                var result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    //var usertemp = UserManager.Users.Where(t => t.openid == openid).FirstOrDefault();
                    var usertemp = UserManager.Users.Where(t => t.unionid == unionid).FirstOrDefault();
                    return new WxResultLoginViewModel()
                    {
                        succeed = true,
                        userid = usertemp.Id,
                        token = token,
                        openid = openid,
                        unionid = unionid,
                    };
                }
            }
            return new WxResultLoginViewModel
            {
                succeed = false,
                openid = openid,
                unionid = unionid,
            };
        }
        public WxResultLoginViewModel Register(WxRegisterViewModel model)
        {
            string token = Guid.NewGuid().ToString();
            var user = UserManager.Users.Where(t=>t.UserName==model.account);
            if (user.Count() != 0)
            {
                return new WxResultLoginViewModel
                {
                    reason = 1,//用户名重复
                    succeed = false,
                };
            }
            var result = UserManager.Create(new M_JiangliUser
            {
                UserName = model.account,
                Email = model.email,
                nickName = model.nickName,
                openid = model.openid,
                Avatar_url = model.avatarUrl,
                RegisterDate = DateTime.Now,
                Gender = model.gender,
                job_id = 0,
                unionid = model.unionid,
            });
            if (result.Succeeded)
            {
                return new WxResultLoginViewModel
                {
                    succeed = true,
                };
            }
            else
            {
                return new WxResultLoginViewModel
                {
                    reason = 0,//用户名不符合规范
                    succeed = false,
                };
            };
        }
        #endregion
        private IAuthenticationManager AuthManager
        {
            get
            {
                return httpContextBase.GetOwinContext().Authentication;
            }
        }
        private jiangliUserManager UserManager
        {
            get
            {
                return httpContextBase.GetOwinContext().GetUserManager<jiangliUserManager>();
            }
        }
        private jiangliRoleManager RoleManager
        {
            get
            {
                return httpContextBase.GetOwinContext().GetUserManager<jiangliRoleManager>();
            }
        }
        //未将对象引用设置到对象实例
        HttpContextBase httpContextBase
        {
            get
            {
                HttpContextWrapper context = new HttpContextWrapper(HttpContext.Current);
                return context;
            }
        }
    }
}
