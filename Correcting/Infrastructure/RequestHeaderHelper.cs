using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Correcting.Infrastructure
{
    public class RequestHeaderHelper
    {
        public RequestHeader GetHeader()
        {
            var model = new RequestHeader();
            model.random = Guid.NewGuid().ToString();
            model.requestTime = DateTime.Now.ToString("yyyy-M-dd H:mm:ss");
            var md5 = new MD5CryptoServiceProvider();
            var result = Encoding.Default.GetBytes(model.random + model.requestTime + ConfigurationManager.AppSettings["ssaas-secret"]);
            model.signature = BitConverter.ToString(md5.ComputeHash(result)).Replace("-", "");
            return model;
        }
    }
    public class RequestHeader
    {
        public string random { get; set; }
        public string requestTime { get; set; }
        public string signature { get; set; }

    }
}