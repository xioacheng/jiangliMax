using DataDeal.Repository.WxRepository;
using jiangliMax.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jiangliMax.Controllers.WebApi
{
    [WxAuthorize]
    public class ExternalController : ApiController
    {
        [HttpGet]
        public string getSinglePoints(int pid)
        {
            return Involve.Current.GetPartPoint(pid);
        }
    }
}
