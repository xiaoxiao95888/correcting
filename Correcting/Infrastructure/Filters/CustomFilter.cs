using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Web.Script.Serialization;
using Correcting.Models;
using System.Security.Cryptography;

namespace Correcting.Infrastructure.Filters
{
    public class CustomAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {            
            bool valid;
            IEnumerable<string> arr;
            filterContext.Request.Headers.TryGetValues("Authorization", out arr);
            if (arr == null)
            {
                valid = false;
            }
            else
            {
                var token = arr.FirstOrDefault();
                CheckToken(token, out valid);
            }
            if (!valid)
            {                
                var errorResponse = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);//InternalServerError
                filterContext.Response = errorResponse;
                return;
            }

            base.OnActionExecuting(filterContext);

        }
        private static void CheckToken(string token, out bool valid)
        {
            valid = false;
            try
            {
                var bpath = Convert.FromBase64String(token);
                token = System.Text.Encoding.Default.GetString(bpath);
                var jsonStr = "{'" + token.Replace("&", "','").Replace("=", "':'") + "'}";
                var serializer = new JavaScriptSerializer();
                var objs = serializer.Deserialize<SignatureModel>(jsonStr);
                var secret = ConfigurationManager.AppSettings["Secret"];               
                var startDateTime = DateTime.Now;
                var endDateTime = startDateTime.AddSeconds(30);
                while (endDateTime >= startDateTime)
                {
                    var signature = SHA1_Hash(objs.Random + endDateTime.ToString("yyyy-M-dd H:mm:ss") + secret);
                    if (objs.Signature.ToUpper() == signature)
                    {
                        valid = true;
                        break;
                    }
                    //if (objs.signature.ToUpper() != signature) return;
                    //valid = true;
                    //var identity = new CustomIdentity(user);
                    //var principal = new CustomPrincipal(identity);
                    //HttpContext.Current.User = principal;
                    endDateTime = endDateTime.AddSeconds(-1);
                }
            }
            catch (Exception)
            {
                valid = false;
            }

        }
        private static string SHA1_Hash(string strSha1In)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesSha1In = System.Text.Encoding.Default.GetBytes(strSha1In);
            var bytesSha1Out = sha1.ComputeHash(bytesSha1In);
            var strSha1Out = BitConverter.ToString(bytesSha1Out);
            strSha1Out = strSha1Out.Replace("-", "").ToUpper();
            return strSha1Out;
        }
    }
}