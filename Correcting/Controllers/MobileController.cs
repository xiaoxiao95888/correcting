using Correcting.Infrastructure;
using Correcting.Models;
using Library.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Correcting.Controllers
{
    public class MobileController : Controller
    {
        // GET: Mobile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Institutions()
        {
            return View();
        }
        public ActionResult SelectParentIns()
        {
            return View();
        }
        public ActionResult SelectLocation()
        {
            return View();
        }
        public ActionResult SelectChildrens()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        
        public ActionResult ScoreBoard()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            
            bool valid = true;
            DateTime time = DateTime.Now;
            if (string.IsNullOrEmpty(model.HeadimgUrl))
            {
                model.HeadimgUrl = "/content/images/HeadimgUrl.jpg";
            }
            if (model == null || string.IsNullOrEmpty(model.EmployeeCode) || string.IsNullOrEmpty(model.Random) || string.IsNullOrEmpty(model.RequestTime) || string.IsNullOrEmpty(model.Signature))
            {
                valid = false;
            }
            if (valid)
            {
                valid = DateTime.TryParse(model.RequestTime, out time);
            }
            if (valid)
            {
               var requestTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["RequestTimeOut"]);
                if (DateTime.Now.AddSeconds(-requestTimeOut) > time)
                {
                    valid = false;
                }
            }
            if (valid)
            {
                var secret = ConfigurationManager.AppSettings["Secret"];
                var selfsignature = SHA1_Hash(model.Random + model.RequestTime + secret);
                if (selfsignature != model.Signature.ToUpper())
                {
                    valid = false;
                }
            }
            if (valid)
            {
                using (var employeeService = new EmployeeService(new Service.DataContext()))
                {
                    var employee = employeeService.GetEmployees().FirstOrDefault(n => n.Code == model.EmployeeCode);
                    if (employee != null)
                    {
                        var employeeModel = new EmployeeModel { Id = employee.Id, Name = employee.Name, Code = employee.Code, Title = employee.Title, HeadImgUrl = model.HeadimgUrl };
                        //CustomIdentity identity = new CustomIdentity(employeeModel);
                        //CustomPrincipal principal = new CustomPrincipal(identity);
                        //HttpContext.User = principal;
                        var identity = UserService.CreateIdentity(employeeModel, DefaultAuthenticationTypes.ApplicationCookie);
                        HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                        return RedirectToAction("Index");
                    }
                }

            }
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
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