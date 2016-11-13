using Correcting.Infrastructure;
using Correcting.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Correcting.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginAdminViewModel model)
        {
           
            if(model.UserName== "admin" && model.Password == "29152916")
            {
                var employeeModel = new EmployeeModel { Name ="admin" ,Code="admin"};
                //CustomIdentity identity = new CustomIdentity(employeeModel);
                //CustomPrincipal principal = new CustomPrincipal(identity);
                //HttpContext.User = principal;
                var identity = UserService.CreateIdentity(employeeModel, DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                return RedirectToAction("Index");
            }
            return View("Login");
        }
    }
}