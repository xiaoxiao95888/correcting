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
            var errormessage = string.Empty;
            try
            {
                var random = filterContext.Request.Headers.GetValues("Random").FirstOrDefault();
                var signature = filterContext.Request.Headers.GetValues("Signature").FirstOrDefault();
                var requestTime = filterContext.Request.Headers.GetValues("RequestTime").FirstOrDefault();
                var time = Convert.ToDateTime(requestTime);
                if (DateTime.Now.AddHours(-10) > time)
                {
                    throw new Exception("overtime");
                }
                CheckToken(random, signature, requestTime, out valid);
            }
            catch (Exception ex)
            {
                valid = false;
                errormessage = ex.Message;
            }
            if (!valid)
            {
                var errorResponse = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, errormessage);//InternalServerError
                filterContext.Response = errorResponse;
                return;
            }
            base.OnActionExecuting(filterContext);

        }
        private static void CheckToken(string random, string signature, string requestTime, out bool valid)
        {
            valid = false;
            try
            {
                var secret = ConfigurationManager.AppSettings["Secret"];
                var selfsignature = SHA1_Hash(random + requestTime + secret);
                if (selfsignature == signature.ToUpper())
                {
                    valid = true;
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