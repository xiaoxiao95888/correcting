using Library.Services;
using Microsoft.Practices.Unity;
using Service.Services;
using System.Web.Http;
using Unity.WebApi;

namespace Correcting
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IInstitutionService, InstitutionService>();
            container.RegisterType<ICorrectingInsService, CorrectingInsService>();
         

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}