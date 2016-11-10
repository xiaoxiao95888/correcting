using Correcting.Infrastructure;
using Correcting.Infrastructure.Filters;
using Correcting.Models;
using Library.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Correcting.Controllers.api
{
    [Custom]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        public LoginController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public object Post(LoginModel model)
        {
            var employee = _employeeService.GetEmployees().Where(n => n.Code == model.EmployeeCode).FirstOrDefault();
            if (employee != null)
            {
                var employeeModel = new EmployeeModel { Id = employee.Id, Name = employee.Name, Code = employee.Code, Title = employee.Title, Headimgurl = model.HeadimgUrl };
                var identity = UserService.CreateIdentity(employeeModel, DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.Current.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                return new
                {
                    verify = true,
                    url = "http://" + Url.Request.Headers.Host + Url.Route("Default", new { controller = "Mobile", action = "Index" })
                };
            }
            return new
            {
                verify = false,
                url = ""
            }; ;
        }
    }
}
