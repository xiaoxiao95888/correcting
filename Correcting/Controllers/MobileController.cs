using Correcting.Infrastructure;
using Correcting.Models;
using Library.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult LoginTest()
        {
            return View();
        }
    }
}