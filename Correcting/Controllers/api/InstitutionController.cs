using Correcting.Infrastructure;
using Correcting.Models;
using Library.Services;
using System;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Correcting.Controllers.api
{
    public class InstitutionController : BaseApiController
    {
        private readonly IInstitutionService _institutionService;
        private readonly ICorrectingInsService _correctingInsService;
        private readonly IEmployeeService _employeeService;
        public static readonly int perpage = Convert.ToInt32(ConfigurationManager.AppSettings["PerPage"]);
        public InstitutionController(IInstitutionService institutionService, ICorrectingInsService correctingInsService, IEmployeeService employeeService)
        {
            _institutionService = institutionService;
            _correctingInsService = correctingInsService;
            _employeeService = employeeService;
        }

        public object Get(Guid id)
        {
            var model = _institutionService.GetInstitution(id);
            if (model != null)
            {
                var correctingIns = _correctingInsService.GetCorrectingInss().FirstOrDefault(n => n.OriginalId == model.Id);
                var parent = (correctingIns != null && correctingIns.ParentId != null) ? _institutionService.GetInstitution(correctingIns.ParentId.Value) : null;
                var childrens = _correctingInsService.GetCorrectingInss().Where(n => n.ParentId == model.Id).Select(n => new InstitutionModel { Id = n.Id, Name = n.Name }).ToArray();

                return new InstitutionModel
                {
                    Id = model.Id,
                    Name = correctingIns != null && correctingIns.Name != null ? correctingIns.Name : model.Name,
                    Address = correctingIns != null && correctingIns.Address != null ? correctingIns.Address : model.Address,
                    TierName = (correctingIns != null && correctingIns.InsLevel != null) ? correctingIns.InsTier : (model.Tier != null ? (model.Tier.Name == "未知" ? "无级别" : model.Tier.Name.ToChineseNumerals() + "级") : null),
                    LevelName = (correctingIns != null && correctingIns.InsLevel != null) ? correctingIns.InsLevel : (model.Level != null ? (model.Level.Name == "未知" ? "无等次" : model.Level.Name) : null),
                    LocationCode = correctingIns != null && correctingIns.LocationCode != null ? correctingIns.LocationCode : model.LocationCode,
                    LocationName = correctingIns != null && correctingIns.LocationName != null ? correctingIns.LocationName : model.LocationName,
                    InstitutionType = correctingIns != null && correctingIns.InsType != null ? correctingIns.InsType : null,
                    Nature = correctingIns != null && correctingIns.InsNature != null ? correctingIns.InsNature : null,
                    Attribute = correctingIns != null && correctingIns.InsAttribute != null ? correctingIns.InsAttribute : null,
                    SpecializedDepartment = correctingIns != null && correctingIns.InsSpecializedDepartment != null ? correctingIns.InsSpecializedDepartment : null,
                    Beds = correctingIns != null && correctingIns.Beds != null ? correctingIns.Beds : null,
                    Outpatients = correctingIns != null && correctingIns.Outpatients != null ? correctingIns.Outpatients : null,
                    TelNum = correctingIns != null && correctingIns.TelNum != null ? correctingIns.TelNum : model.TelNum,
                    Childrens = childrens,
                    Parent = parent != null ? new InstitutionModel { Id = parent.Id, Name = parent.Name } : null,

                };
            }
            return null;
        }
        public object Get()
        {
            var key = HttpContext.Current.Request["key"];
            var strpage = HttpContext.Current.Request["page"];
            var page = 1;
            if (int.TryParse(strpage, out page))
            {
                if (page < 1)
                {
                    page = 1;
                }
            }
            else
            {
                page = 1;
            }
            var data = _institutionService.GetInstitutions().Where(n => n.IsDeleted == false && n.IsApproved && n.Name.Contains(key));
            var sumcount = data.Count();
            var dbdata = data.OrderBy(n => n.Name).Skip((page - 1) * perpage).Take(perpage).ToList();
            var model = new InstitutionData
            {
                InstitutionModels = dbdata.Select(n => new InstitutionModel
                {
                    Id = n.Id,
                    Name = n.Name,
                }).ToArray(),
                Page = page,
                PerPage = perpage,
                PageCount = (sumcount / perpage) + (sumcount % perpage == 0 ? 0 : 1),
                RecordCount = sumcount
            };
            return model;
        }
    }
}
