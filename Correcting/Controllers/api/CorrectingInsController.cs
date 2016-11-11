using Correcting.Infrastructure;
using Correcting.Models;
using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Correcting.Controllers.api
{
    public class CorrectingInsController : BaseApiController
    {
        private readonly ICorrectingInsService _correctingInsService;
        private readonly IEmployeeService _employeeService;
        public CorrectingInsController(ICorrectingInsService correctingInsService, IEmployeeService employeeService)
        {
            _correctingInsService = correctingInsService;
            _employeeService = employeeService;
        }
        public object Post(InstitutionModel model)
        {
            var currentUser = User.Identity.GetEmoloyee();
            //当前存在的数据(删除)
            var existing = _correctingInsService.GetCorrectingInss().FirstOrDefault(n => n.OriginalId == model.Id);
            if (existing != null && existing.EmpCode != currentUser.Code)
            {
                var employee = _employeeService.GetEmployees().Where(n => n.Code == existing.EmpCode).FirstOrDefault();
                var res = new ResponseModel { Error = true, Message = "" };
                if (employee != null)
                {
                    res.Message = employee.Name + "抢先一步正在更新";
                    return res;
                }
            }
            if (existing != null && existing.EmpCode == currentUser.Code)
            {
                var tasks = _correctingInsService.GetCorrectingInss().Where(n => n.TaskId == existing.TaskId);
                foreach (var task in tasks)
                {
                    task.IsDeleted = true;
                    task.DateTime = DateTime.Now;
                }
            }
            var taskId = Guid.NewGuid();
            //先添加childs
            if (model.Childrens != null)
            {
                foreach (var child in model.Childrens)
                {
                    _correctingInsService.Insert(new CorrectingIns
                    {
                        Id = Guid.NewGuid(),
                        TaskId = taskId,
                        OriginalId = child.Id == Guid.Empty ? (Guid?)null : child.Id,
                        Name = child.Name != null ? child.Name : null,
                        Address = child.Address != null ? child.Address : null,
                        InsLevel = child.LevelName != null ? child.LevelName : null,
                        InsType = child.InstitutionType != null ? child.InstitutionType : null,
                        InsAttribute = child.Attribute != null ? child.Attribute : null,
                        InsNature = child.Nature != null ? child.Nature : null,
                        InsSpecializedDepartment = child.SpecializedDepartment != null ? child.SpecializedDepartment : null,
                        TelNum = child.TelNum != null ? child.TelNum : null,
                        Outpatients = child.Outpatients != null ? child.Outpatients : null,
                        Beds = child.Beds != null ? child.Beds : null,
                        ParentId = model.Id,
                        DateTime = DateTime.Now,
                        EmpCode = currentUser.Code,
                        EmpName = currentUser.Name,
                        LocationCode = child.LocationCode,
                        LocationName = child.LocationName,
                        IsPrimary = false
                    });
                }
            }
            //再添加Institution
            _correctingInsService.Insert(new CorrectingIns
            {
                Id = Guid.NewGuid(),
                IsPrimary = true,
                TaskId = taskId,
                OriginalId = model.Id,
                Name = model.Name,
                Address = model.Address != null ? model.Address : null,
                InsLevel = model.LevelName != null ? model.LevelName : null,
                InsType = model.InstitutionType != null ? model.InstitutionType : null,
                InsAttribute = model.Attribute != null ? model.Attribute : null,
                InsNature = model.Nature != null ? model.Nature : null,
                InsSpecializedDepartment = model.SpecializedDepartment != null ? model.SpecializedDepartment : null,
                TelNum = model.TelNum != null ? model.TelNum : null,
                Outpatients = model.Outpatients != null ? model.Outpatients : null,
                Beds = model.Beds != null ? model.Beds : null,
                ParentId = model.Parent != null ? model.Parent.Id : (Guid?)null,
                DateTime = DateTime.Now,
                EmpCode = currentUser.Code,
                EmpName = currentUser.Name,
                LocationCode = model.LocationCode,
                LocationName = model.LocationName,
            });

            try
            {
                _correctingInsService.Update();
            }
            catch (Exception ex)
            {
                return new ResponseModel { Error = true };
            }


            return new ResponseModel { Error = false };
        }
    }
}
