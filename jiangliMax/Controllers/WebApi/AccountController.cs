using DataDeal.Models.RepositoryModal;
using DataDeal.Models.WxModels;
using DataDeal.Repository;
using DataDeal.Repository.WxRepository;
using DataDeal.WholeSituation;
using jiangliMax.Attribute;
using jiangliMax.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jiangliMax.Controllers.WebApi
{
    [WxAuthorize]
    public class AccountController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public WxResultLoginViewModel Login()
        {
            UserInfo dbuser = new UserInfo();
            LoginResult result = null;
            string unionid = null;
            try
            {
                string code = GetHeader(ConstantLogin.WX_HEADER_CODE);
                string iv = GetHeader(ConstantLogin.WX_HEADER_IV);
                string encrytedData = GetHeader(ConstantLogin.WX_HEADER_ENCRYPTED_DATA);
                result = GetUsersHelper.GetOpenId(code);
                if (result.Openid == null)
                {
                    return null;
                }
                //获取unionid
                string session_key = result.Skey;
                GetUsersHelper.AesIV = iv;
                GetUsersHelper.AesKey = session_key;
                string result2 = GetUsersHelper.AESDecrypt(encrytedData);
                JObject _usrInfo = (JObject)JsonConvert.DeserializeObject(result2);
                unionid = _usrInfo["unionId"].ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
            string token = Guid.NewGuid().ToString();
            return dbuser.Login(token,result.Openid,unionid);
        }
        [AllowAnonymous]
        [HttpPost]
        public WxResultLoginViewModel Register(WxRegisterViewModel model)
        {
            UserInfo dbuser = new UserInfo();
            return dbuser.Register(model);
        }
        [HttpGet]
        public DataDeal.Models.WxModels.Case.WxUserViewModel GetUserById(string userId)
        {
            var user = UserInfo.Current.GetUserById(userId);
            var caseitem = new DataDeal.Models.WxModels.Case.WxUserViewModel()
            {
                avatar_url = user.Avatar_url,
                case_number = CaseList.Current.m_getCasesByUserid(userId).Count(),
                gender = user.Gender,
                description = user.SelfIntroduction,
                headline = user.Hobby,
                id = user.Id,
                name = user.nickName,
                weight = user.Weight,
                caselist = CaseList.Current.getCaseByUserIdBreif(userId),
            };
            return caseitem;
        }
        public List<dynamic> GetUsersByNickname(string q)
        {
            var userlist = UserInfo.Current.GetUsersByNickname(q);
            return _UsersByNickName(userlist);
        }
        #region 内部方法
        public List<dynamic> _UsersByNickName(IEnumerable<BasicUserViewModel> userlist)
        {
            List<dynamic> result = new List<dynamic>();
            foreach (var item in userlist)
            {
                var temp = new
                {
                    avatar_url=item.avatar_url,
                    id=item.id,
                    nickname=item.nickname,
                    numbercase=item.numbercase.Count(),
                    weight=item.weight,
                };
                result.Add(temp);
            }
            return result;
        }
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="headerName"></param>
        /// <returns></returns>
        [NonAction]
        private string GetHeader(string headerName)
        {
            var headerValue = Request.Headers.GetValues(headerName).FirstOrDefault();

            if (String.IsNullOrEmpty(headerValue))
            {
                return null;
                //Ilog.Debug("获取[" + headerName + "的请求头失败");
            }

            return headerValue;
        }
        #endregion
    }
}
