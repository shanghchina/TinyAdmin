using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Domain
{

    /// <summary>
    /// 产品中心--产品信息表
    /// </summary>
    [Table("T_POC_Product", DBName = EumDBName.POC)]
    public class T_POC_Product : EntityBase
    {

        /// <summary>
        /// FK提取器明细表guid
        /// </summary>
        public Guid FKDetailGuid { get; set; }  //FK提取器明细表guid	

        /// <summary>
        /// 产品信息表guid
        /// </summary>
        public Guid ProductGuid { get; set; }   //产品信息表guid	

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNo { get; set; }   //产品编号	50

        /// <summary>
        /// 产品分类Guid
        /// </summary>
        public Guid ProductTypeGuid { get; set; }   //产品分类Guid	

        /// <summary>
        /// 产品分类名称
        /// </summary>
        public string ProductTypeName { get; set; } //产品分类名称	100

        /// <summary>
        /// 产品分类显示名称
        /// </summary>
        public string ProductTypeLable { get; set; }    //产品分类显示名称	100

        /// <summary>
        /// 来源系统
        /// </summary>
        public int FromSystem { get; set; } //来源系统	

        /// <summary>
        /// 所属一级组织id
        /// </summary>
        public Guid TeachLevelOneOrgID { get; set; }    //所属一级组织id	

        /// <summary>
        /// 所属一级组织名称
        /// </summary>
        public string TeachLevelOneOrgName { get; set; }    //所属一级组织名称	50

        /// <summary>
        /// 来源系统的课程id
        /// </summary>
        public string CourseID { get; set; }    //来源系统的课程id	100

        /// <summary>
        /// 来源系统的课程名称
        /// </summary>
        public string CourseName { get; set; }  //来源系统的课程名称	100

        /// <summary>
        /// 来源系统的所属产品大类1级id
        /// </summary>
        public string ProductTypeOneID { get; set; }    //来源系统的所属产品大类1级id	100

        /// <summary>
        /// 来源系统的所属产品大类2级id
        /// </summary>
        public string ProductTypeTwoID { get; set; }    //来源系统的所属产品大类2级id	100

        /// <summary>
        /// 来源系统的年份
        /// </summary>
        public int CourseYear { get; set; } //来源系统的年份	

        /// <summary>
        /// 来源系统的期段id
        /// </summary>
        public string TermID { get; set; }  //来源系统的期段id	100

        /// <summary>
        /// 期段名称
        /// </summary>
        public string TermName { get; set; }    //期段名称	100

        /// <summary>
        /// 来源系统的所属年级id
        /// </summary>
        public string GradeID { get; set; } //来源系统的所属年级id	100

        /// <summary>
        /// 所属年级名称
        /// </summary>
        public string GradeName { get; set; }   //所属年级名称	100

        /// <summary>
        /// 来源系统的班型id
        /// </summary>
        public string ClassTypeID { get; set; } //来源系统的班型id	100

        /// <summary>
        /// 班型名称
        /// </summary>
        public string ClassTypeName { get; set; }   //班型名称	100

        /// <summary>
        /// 来源系统的课程类型id
        /// </summary>
        public string FlagID { get; set; }  //来源系统的课程类型id	100

        /// <summary>
        /// 课程类型名称
        /// </summary>
        public string FlagName { get; set; }    //课程类型名称	100

        /// <summary>
        /// 来源系统的所属科目id
        /// </summary>
        public string SubjectID { get; set; }   //来源系统的所属科目id	100

        /// <summary>
        /// 所属科目名称
        /// </summary>
        public string SubjectName { get; set; } //所属科目名称	100

        /// <summary>
        /// 来源系统的所属类型id
        /// </summary>
        public string CategoryID { get; set; }  //来源系统的所属类型id	100

        /// <summary>
        /// 所属类型名称
        /// </summary>
        public string CategoryName { get; set; }    //所属类型名称	100

        /// <summary>
        /// 计划总课时数、计划总次数
        /// </summary>
        public decimal TotalClassHour { get; set; } //计划总课时数、计划总次数	18

        /// <summary>
        /// 计划总课时数的单位（“小时”或“次”）
        /// </summary>
        public string TotalClassHourName { get; set; }  //计划总课时数的单位（“小时”或“次”）	100

        /// <summary>
        /// 学费课时单价
        /// </summary>
        public decimal FeeUnitPrice { get; set; }   //学费课时单价	18

        /// <summary>
        /// 学费课时单价的单位（“元/小时”或“元/期”或“元/次”）
        /// </summary>
        public string FeeUnitPriceName { get; set; }    //学费课时单价的单位（“元/小时”或“元/期”或“元/次”）	100

        /// <summary>
        /// 课程状态
        /// </summary>
        public int CourseStatus { get; set; }   //课程状态(-1：已删除、0：无效、1：有效）	

        /// <summary>
        /// 业务创建日期
        /// </summary>
        public DateTime ProductCreatedDate { get; set; }    //业务创建日期	

        /// <summary>
        /// 业务更新日期
        /// </summary>
        public DateTime ProductUpdateDate { get; set; } //业务更新日期	

        /// <summary>
        /// 产品中心的所属年级id
        /// </summary>
        public Guid GradeDictGuid { get; set; } //产品中心的所属年级id	

        /// <summary>
        /// 产品中心所属年级名称
        /// </summary>
        public string GradeDictName { get; set; }   //产品中心所属年级名称	100

        /// <summary>
        /// 产品中心的所属类型id
        /// </summary>
        public Guid CategoryDictGuid { get; set; }  //产品中心的所属类型id	

        /// <summary>
        /// 产品中心所属类型名称
        /// </summary>
        public string CategoryDictName { get; set; }    //产品中心所属类型名称	100

        /// <summary>
        /// 产品中心的所属科目id
        /// </summary>
        public Guid SubjectDictGuid { get; set; }   //产品中心的所属科目id	

        /// <summary>
        /// 产品中心所属科目名称
        /// </summary>
        public string SubjectDictName { get; set; } //产品中心所属科目名称	100

        /// <summary>
        /// 产品中心的期段id
        /// </summary>
        public Guid TermDictGuid { get; set; }  //产品中心的期段id	

        /// <summary>
        /// 产品中心期段名称
        /// </summary>
        public string TermDictName { get; set; }    //产品中心期段名称	100

        /// <summary>
        /// 产品中心的班型id
        /// </summary>
        public Guid ClassTypeDictGuid { get; set; } //产品中心的班型id	

        /// <summary>
        /// 产品中心班型名称
        /// </summary>
        public string ClassTypeDictName { get; set; }   //产品中心班型名称	100

        /// <summary>
        /// 备注
        /// </summary>
        public string Describe { get; set; }    //备注	1,000

        /// <summary>
        /// 授权校区数 例如：121个校区
        /// </summary>

        public string CampusCountName { get; set; } //授权校区数	20


        ///// <summary>
        ///// 修改人
        ///// </summary>
        //public string UpdaterUserId { get; set; }
        ///// <summary>
        ///// 修改人
        ///// </summary>
        //public string UpdaterUserName { get; set; }


        /// <summary>
        /// 关联产品Id集合
        /// </summary>
        [NoMapper]
        public List<Guid> RelationIds { get; set; }

    }
}

