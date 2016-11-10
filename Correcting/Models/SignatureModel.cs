using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Models
{
    public class SignatureModel
    {
        /// <summary>
        /// 随机数
        /// </summary>
        public string Random { get; set; }
        /// <summary>
        /// 生成后的签名random+requestTime+secret 然后Hash
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 请求时间
        /// </summary>
        public string RequestTime { get; set; }        
    }
}