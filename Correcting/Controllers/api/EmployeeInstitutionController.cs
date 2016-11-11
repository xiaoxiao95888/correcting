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
            var emp = _employeeService.GetEmployees().Where(n => !n.IsDeleted).Where(n => n.Code == user.Code).FirstOrDefault();
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
                InstitutionModels = emp.EmployeeTerritories.Where(p => p.StartDate <= DateTime.Now && (p.EndDate >= DateTime.Now || p.EndDate == null)).SelectMany(p => p.Territory.TerritorySalesPositions.Where(s => s.IsDeleted == false && s.IsDeleted == false && s.StartDate <= DateTime.Now && (s.EndDate == null || s.EndDate >= DateTime.Now) && s.SalesPosition.Institution.IsDeleted == false && s.SalesPosition.Institution.IsApproved)).Select(p => new InstitutionModel
                {
                    Id = p.SalesPosition.Institution.Id,
                    Name = p.SalesPosition.Institution.Name,
                    Address = p.SalesPosition.Institution.Address,
                    LevelName = (p.SalesPosition.Institution.Level != null && p.SalesPosition.Institution.Tier != null) ? (p.SalesPosition.Institution.Tier.Name.ToChineseNumerals() + "级" + p.SalesPosition.Institution.Level.Name) : null,
                }).ToArray()
            };
            return model;
        }
    }
}
