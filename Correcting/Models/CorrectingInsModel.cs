using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Models
{
    public class CorrectingInsModel
    {
        public Guid TaskId { get; set; }
        public EmployeeModel EmployeeModel { get; set; }
        public InstitutionModel InstitutionModel { get; set; }
        public DateTime DateTime { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsDeleted { get; set; }
    }
}