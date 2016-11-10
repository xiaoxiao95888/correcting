using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Models
{
    public class InstitutionModel
    {
        /// <summary>
        /// 机构Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string Name { get; set; }     
        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelNum { get; set; }
        /// <summary>
        /// 地理信息
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// 地理信息名称
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 专科方向
        /// </summary>
        public string SpecializedDepartment  { get; set; }
        /// <summary>
        /// 医院等级
        /// </summary>       
        public string LevelName { get; set; }        
        /// <summary>
        /// 机构性质(公立、民营)
        /// </summary>
        public string Nature { get; set; }
        /// <summary>
        /// 医院属性(普通医院、厂矿医院、军警医院)
        /// </summary>
        public string Attribute { get; set; }
        /// <summary>
        /// 机构类型(综合医院、专科医院、基层医疗机构)
        /// </summary>
        public string InstitutionType { get; set; }
        /// <summary>
        /// 床位数
        /// </summary>
        public int? Beds { get; set; }
        /// <summary>
        /// 门诊人数
        /// </summary>
        public int? Outpatients { get; set; }
        public bool Checked { get { return true; } }
        public InstitutionModel Parent { get; set; }
        public InstitutionModel[] Childrens { get; set; }

    }
    
}