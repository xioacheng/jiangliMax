using DataDeal.Repository;
using DataDeal.WholeSituation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace jiangliMax.Attribute
{
    public class WxAuthorize: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            var request = actionContext.Request;
            try
            {
                string token = request.Headers.GetValues(ConstantLogin.WX_HEADER_TOKEN).FirstOrDefault();
                string id = request.Headers.GetValues(ConstantLogin.WX_HEADER_ID).FirstOrDefault();
                if (!String.IsNullOrEmpty(token))
                {
                    bool result = UserInfo.Current.findToken(id,token);
                    if (result)
                    {
                        return;
                    }
                }
                throw new Exception();
            }
            catch (Exception e)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}