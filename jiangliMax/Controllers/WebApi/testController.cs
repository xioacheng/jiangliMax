using jiangliMax.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;

namespace jiangliMax.Controllers.WebApi
{
    public class testController : ApiController
    {
        [HttpGet]
        public JsonResult<string> get()
        {
            //Guid全球唯一标识码128位
            string token = Guid.NewGuid().ToString();
            return Json("123");
        }
    }
}
