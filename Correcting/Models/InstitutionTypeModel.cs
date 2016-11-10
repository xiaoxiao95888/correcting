using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Models
{
    /// <summary>
    /// 机构类型
    /// </summary>
    public class InstitutionTypeModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }
    }
}