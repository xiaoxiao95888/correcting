using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Models
{
    public class CorrectingInsModel
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public int TaskCode { get; set; }
        public EmployeeModel EmployeeModel { get; set; }
        public InstitutionModel InstitutionModel { get; set; }
        public InstitutionModel OriginalInstitutionModel { get; set; }
        public bool WhetherToAdd { get; set; }
        /// <summary>
        /// 主要的
        /// </summary>
        public bool IsPrimary { get; set; }
        public DateTime DateTime { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsDeleted { get; set; }
    }
}