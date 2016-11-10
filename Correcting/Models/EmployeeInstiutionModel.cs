using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Models
{
    public class EmployeeInstiutionModel
    {
        public EmployeeModel EmployeeModel { get; set; }       
        public InstitutionModel[] InstitutionModels { get; set; }
    }
}