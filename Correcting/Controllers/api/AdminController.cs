using Correcting.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Correcting.Controllers.api
{
    [AllowAnonymous]
    public class AdminController : BaseApiController
    {
        private readonly IInstitutionService _institutionService;
        private readonly ICorrectingInsService _correctingInsService;
        private readonly IEmployeeService _employeeService;
        public AdminController(IInstitutionService institutionService, ICorrectingInsService correctingInsService, IEmployeeService employeeService)
        {
            _institutionService = institutionService;
            _correctingInsService = correctingInsService;
            _employeeService = employeeService;
        }
        public object Get()
        {
            var allData = _correctingInsService.GetCorrectingInss().ToList();
            var models = allData.Select(n => new CorrectingInsModel
            {
                TaskId = n.TaskId,
                EmployeeModel = new EmployeeModel { Name = n.EmpName, Code = n.EmpCode },
                DateTime = n.DateTime,
                InstitutionModel = new InstitutionModel
                {
                    Id = n.OriginalId.Value,
                    Name = n.Name,
                    TelNum = n.TelNum,
                    LocationCode = n.LocationCode,
                    LocationName = n.LocationName,
                    Address = n.Address,
                    SpecializedDepartment = n.InsSpecializedDepartment,
                    LevelName = n.InsLevel,
                    Nature = n.InsNature,
                    Attribute = n.InsAttribute,
                    InstitutionType = n.InsType,
                    Beds = n.Beds,
                    Outpatients = n.Outpatients,
                    Parent = n.ParentId != null ? allData.Where(a => a.OriginalId == n.ParentId).Select(a => new InstitutionModel
                    {
                        Name = a.Name,
                        LocationName = a.LocationName,
                    }).FirstOrDefault() : null
                },
                IsApproved = n.IsApproved,
                IsDeleted = n.IsDeleted
            });
            return models;
        }
    }
}
