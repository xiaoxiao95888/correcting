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
    /// <summary>
    /// 积分榜
    /// </summary>
    [AllowAnonymous]
    public class ScoreBoardController : BaseApiController
    {
        private readonly IInstitutionService _institutionService;
        private readonly ICorrectingInsService _correctingInsService;
        private readonly IEmployeeService _employeeService;
        public ScoreBoardController(IInstitutionService institutionService, ICorrectingInsService correctingInsService, IEmployeeService employeeService)
        {
            _institutionService = institutionService;
            _correctingInsService = correctingInsService;
            _employeeService = employeeService;
        }
        public object Get()
        {
            var source = _correctingInsService.GetCorrectingInss().Where(n => n.IsApproved == true).GroupBy(n => n.EmpCode).Select(n => new ScoreBoardModel { EmpCode = n.Key, Score = n.GroupBy(p => p.TaskId).Count(), EmpName = n.Select(p => p.EmpName).FirstOrDefault() }).OrderBy(n => n.Score).Take(20).ToList();           
           
            return source;

        }
    }
}
