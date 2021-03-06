﻿using Correcting.Infrastructure;
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
                var allData = _correctingInsService.GetCorrectingInss().Where(n => n.IsApproved != true).OrderBy(n => n.TaskCode).ThenByDescending(n=>n.IsPrimary).ToList();
                var models = allData.Select(n => new CorrectingInsModel
                {
                    Id = n.Id,
                    TaskId = n.TaskId,
                    TaskCode = n.TaskCode,
                    IsPrimary=n.IsPrimary,
                    EmployeeModel = new EmployeeModel { Name = n.EmpName, Code = n.EmpCode },
                    DateTime = n.DateTime,
                    InstitutionModel = new InstitutionModel
                    {
                        Name = n.Name != null ? n.Name : (n.OriginalInstitution != null ? n.OriginalInstitution.Name : n.Name),
                        TelNum = n.TelNum != null ? n.TelNum : (n.OriginalInstitution != null ? n.OriginalInstitution.TelNum : n.TelNum),
                        LocationCode = n.LocationCode != null ? n.LocationCode : (n.OriginalInstitution != null ? n.OriginalInstitution.LocationCode : n.LocationCode),
                        LocationName = n.LocationName != null ? n.LocationName : (n.OriginalInstitution != null ? n.OriginalInstitution.LocationName : n.LocationName),
                        Address = n.Address != null ? n.Address : (n.OriginalInstitution != null ? n.OriginalInstitution.Address : n.Address),
                        SpecializedDepartment = n.InsSpecializedDepartment,
                        LevelName = n.InsLevel != null ? n.InsLevel : ((n.OriginalInstitution != null && n.OriginalInstitution.LevelId != null) ? n.OriginalInstitution.LevelName : n.InsLevel),
                        TierName = n.InsTier != null ? n.InsTier : ((n.OriginalInstitution != null && n.OriginalInstitution.TierId != null) ? n.OriginalInstitution.TierName.ToChineseNumerals() : n.InsTier),
                        Nature = n.InsNature,
                        Attribute = n.InsAttribute,
                        InstitutionType = n.InsType,
                        Beds = n.Beds,
                        Outpatients = n.Outpatients,
                        Parent = n.ParentId != null ? new InstitutionModel { Name = n.ParentInstitution.Name, LocationName = n.ParentInstitution.LocationName } : null,
                        Childrens = allData.Where(p => p.ParentId == n.OriginalId && n.OriginalId != null).Select(p => new InstitutionModel { Name = p.Name }).ToArray()
                    },
                    OriginalInstitutionModel = n.OriginalId != null ? new InstitutionModel
                    {
                        Name = n.OriginalInstitution.Name,
                        TelNum = n.OriginalInstitution.TelNum,
                        LocationCode = n.OriginalInstitution.LocationCode,
                        LocationName = n.OriginalInstitution.LocationName,
                        Address = n.OriginalInstitution.Address,
                        LevelName = n.OriginalInstitution.LevelName == "未知" ? "无等次" : n.OriginalInstitution.LevelName,
                        TierName = n.OriginalInstitution.TierId != null ? (n.OriginalInstitution.TierName == "未知" ? "无级别" : n.OriginalInstitution.TierName.ToChineseNumerals() + "级") : null
                    } : null,
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
