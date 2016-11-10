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
        public CorrectingInsController(ICorrectingInsService correctingInsService)
        {
            _correctingInsService = correctingInsService;
        }
        public object Post(InstitutionModel model)
        {
            var existing = _correctingInsService.GetCorrectingInsByOriginalId(model.Id);
            if (existing != null && existing.EmpCode=="")
            {

            }
            ////新增的
            //var createins = model.InstitutionModels.Where(n => n.Id == null);
            //foreach (var item in createins)
            //{
            //    _correctingInsService.Insert(new CorrectingIns
            //    {
            //        Id = Guid.NewGuid(),
                    
            //    }
            //    );
            //}
            return null;
        }
    }
}
