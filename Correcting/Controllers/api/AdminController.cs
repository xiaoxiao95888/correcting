using Correcting.Infrastructure;
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
            var user = User.Identity.GetEmoloyee();
            if (user.Name == "admin")
            {
                var allData = _correctingInsService.GetCorrectingInss().Where(n=>n.IsApproved!=true).OrderBy(n => n.DateTime).ToList();
                var models = allData.Select(n => new CorrectingInsModel
                {
                    Id = n.Id,
                    TaskId = n.TaskId,
                    TaskCode = n.TaskCode,
                    EmployeeModel = new EmployeeModel { Name = n.EmpName, Code = n.EmpCode },
                    DateTime = n.DateTime,
                    InstitutionModel = new InstitutionModel
                    {
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
                        Parent = n.ParentId != null ? new InstitutionModel { Name = n.ParentInstitution.Name, LocationName = n.ParentInstitution.LocationName } : null

                    },
                    WhetherToAdd = n.OriginalId == null,
                    IsApproved = n.IsApproved,
                    IsDeleted = n.IsDeleted
                });
                return models;
            }
            return null;
        }
        public object Post(CorrectingInsModel model)
        {
            var user = User.Identity.GetEmoloyee();
            if (user.Name == "admin")
            {
                var item = _correctingInsService.GetCorrectingIns(model.Id);
                if (item != null)
                {
                    item.IsApproved = model.IsApproved;
                    item.IsDeleted = model.IsDeleted;
                    _correctingInsService.Update();
                }
            }
            return null;
        }
    }
}
