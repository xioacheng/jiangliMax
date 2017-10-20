using DataDeal.WholeSituation;
using jiangliMax.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace jiangliMax.Controllers.WebApi
{
    public class GetUsersHelper
    {
        public static LoginResult GetOpenId(string code)
        {
            WebClient wb = new WebClient();
            string url = "https://api.weixin.qq.com/sns/jscode2session?"
            + "appid=" + GlobalConfig.appid
            + "&secret=" + GlobalConfig.secret
            + "&grant_type=authorization_code&js_code=" + code;
            try
            {
                Stream sc = wb.OpenRead(url);
                StreamReader sr = new StreamReader(sc);
                string result = sr.ReadToEnd();
                LoginResult li = JsonConvert.DeserializeObject<LoginResult>(result);
                sc.Close(); sr.Close();
                return li;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string GetUrltoHtml(string Url, string type)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance.  
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream)  
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
        #region 微信小程用户数据解密

        public static string AesKey;
        public static string AesIV;

        public static string AESDecrypt(string inpudata)
        {
            try
            {
                AesIV = AesIV.Replace(" ", "+");
                AesKey = AesKey.Replace(" ", "+");
                inpudata = inpudata.Replace(" ", "+");
                byte[] encryptedData = Convert.FromBase64String(inpudata);
                RijndaelManaged rijn = new RijndaelManaged();
                rijn.Key = Convert.FromBase64String(AesKey);
                rijn.IV = Convert.FromBase64String(AesIV);
                rijn.Mode = CipherMode.CBC;
                rijn.Padding = PaddingMode.PKCS7;
                ICryptoTransform trasform = rijn.CreateDecryptor();
                byte[] plainText = trasform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                string result = Encoding.UTF8.GetString(plainText);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion
    }
}