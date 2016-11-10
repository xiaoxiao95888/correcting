using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Models
{
    public class InstitutionData
    {
        public InstitutionModel[] InstitutionModels { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PerPage { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }
    }
}