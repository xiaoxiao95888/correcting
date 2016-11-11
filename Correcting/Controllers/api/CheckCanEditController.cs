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
    public class CheckCanEditController : BaseApiController
    {
        private readonly IInstitutionService _institutionService;
        private readonly ICorrectingInsService _correctingInsService;
        private readonly IEmployeeService _employeeService;       
        public CheckCanEditController(IInstitutionService institutionService, ICorrectingInsService correctingInsService, IEmployeeService employeeService)
        {
            _institutionService = institutionService;
            _correctingInsService = correctingInsService;
            _employeeService = employeeService;
        }
        /// <summary>
        /// 检查是否允许编辑
        /// </summary>
        /// <param name="id">InstitutionId</param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            var currentUser = User.Identity.GetEmoloyee();
            var res = new ResponseModel { Error=false};
            //当前存在的数据
            var existing = _correctingInsService.GetCorrectingInss().FirstOrDefault(n => n.OriginalId == id);
            if (existing != null && existing.EmpCode != currentUser.Code)
            {
                var employee = _employeeService.GetEmployees().Where(n => n.Code == existing.EmpCode).FirstOrDefault();
                
                if (employee != null)
                {
                    res.Error = true;
                    res.Message = employee.Name + "抢先一步正在更新";
                    return res;
                }
            }
            return res;
        }
    }
}
