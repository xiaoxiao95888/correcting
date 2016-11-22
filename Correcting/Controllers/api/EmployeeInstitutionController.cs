using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.Services;
using Correcting.Models;
using Correcting.Infrastructure;
using System.Web;
using System.Security.Claims;

namespace Correcting.Controllers.api
{
    public class EmployeeInstitutionController : BaseApiController
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeInstitutionController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public object Get()
        {
            var user = User.Identity.GetEmoloyee();
            var emp = _employeeService.GetEmployees().Where(n => !n.IsDeleted).FirstOrDefault(n => n.Code == user.Code);
            var datenow=new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
            var model = new EmployeeInstiutionModel
            {
                EmployeeModel = new EmployeeModel
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Title = emp.Title,
                    Code = emp.Code,
                    HeadImgUrl = user.HeadImgUrl
                },
                InstitutionModels = emp.EmployeeTerritories.Where(p => p.StartDate <= datenow && (p.EndDate >= datenow || p.EndDate == null)).SelectMany(p => p.Territory.TerritorySalesPositions.Where(s => s.IsDeleted == false &&  s.StartDate <= datenow && (s.EndDate == null || s.EndDate >= datenow) && s.SalesPosition.Institution.IsDeleted == false && s.SalesPosition.Institution.IsApproved)).Select(p => new InstitutionModel
                {
                    Id = p.SalesPosition.Institution.Id,
                    Name = p.SalesPosition.Institution.Name,
                    Address = p.SalesPosition.Institution.Address,
                    //LevelName = p.SalesPosition.Institution.Level != null ? (p.SalesPosition.Institution.Level.Name == "未知" ? "无等次" : p.SalesPosition.Institution.Level.Name) : null,
                    //TierName = p.SalesPosition.Institution.Tier != null ? (p.SalesPosition.Institution.Tier.Name == "未知" ? "无级别" : p.SalesPosition.Institution.Tier.Name.ToChineseNumerals() + "级") : null
                }).Distinct(p=>p.Id).OrderBy(n => n.Name).ToArray()
            };
            return model;
        }
    }
    
}
