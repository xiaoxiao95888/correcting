using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Models
{
    public class LoginModel
    {
        public string Random { get; set; }
        public string Signature { get; set; }
        public string RequestTime { get; set; }
        public string EmployeeCode { get; set; }
        public string HeadimgUrl { get; set; }
    }
}