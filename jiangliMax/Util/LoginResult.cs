using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangliMax.Util
{
    public class LoginResult
    {
        [JsonProperty("openid")]
        public string Openid { get; set; }

        [JsonProperty("session_key")]
        public string Skey { get; set; }
    }
}