using Tiny.OPS.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 班级信息viewModel
    /// </summary>
    public class VM_EXT_Class
    {
        /// <summary>
        /// 来源系统
        /// </summary>
        [Display(Name = "系统来源")]
        [NoExport]
        public int FromSystem { get; set; }


        /// <summary>
        /// 产品中心来源系统
        /// </summary>
        [Display(Name = "系统来源")]
        [NoExport]
        public int POCSource { get; set; }

        /// <summary>
        /// 产品中心来源系统
        /// </summary>
        [Display(Name = "系统来源")]
        public string POCSourceName { get; set; }

        /// <summary>
        /// 所属一级组织名称
        /// </summary>
        [Display(Name = "所属事业部")]
        public string TeachLevelOneOrgName { get; set; }

        /// <summary>
        /// 所属上课校区对应的末级组织名称
        /// </summary>
        [Display(Name = "末级组织")]
        public string TeachNetOrgName { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        [Display(Name = "班级名称")]
        public string ClassName { get; set; }

        /// <summary>
        /// 所属课程
        /// </summary>
        [Display(Name = "所属课程")]
        public string CourseName { get; set; }

        /// <summary>
        /// 上课老师
        /// </summary>
        [Display(Name = "上课老师")]
        public string TeacherUserName { get; set; }


        /// <summary>
        /// 预招最小人数（如没有该值的，则默认为1）
        /// </summary>
        [Display(Name = "人数")]
        public int MaxStudentAmoun { get; set; }

        /// <summary>
        /// 计划开班日期
        /// </summary>
        [Display(Name = "计划开班日期")]
        public DateTime? OpenDate { get; set; }

        /// <summary>
        /// 计划结课日期
        /// </summary>
        [Display(Name = "计划结业时期")]
        public DateTime? CloseDate { get; set; }

        /// <summary>
        /// 计划总课时数
        /// </summary>
        [Display(Name = "计划总课时数")]
        public decimal TotalClassHour { get; set; }

        /// <summary>
        /// 计划总课时数、计划总次数
        /// </summary>
        [Display(Name = "总课时数的单位")]
        public string TotalClassHourName { get; set; }

        /// <summary>
        /// 业务更新日期
        /// </summary>
        [Display(Name = "更新日期")]
        public DateTime ProductUpdateDate { get; set; }

        /// <summary>
        /// 来源系统的校区id
        /// </summary>
        [NoExport]
        public string CampusID { get; set; }

        /// <summary>
        /// 所属上课校区对应的末级组织id
        /// </summary>
        [Display(Name = "末级组织id")]
        [NoExport]
        public Guid TeachNetOrgID { get; set; }

        /// <summary>
        /// 所属上课校区对应的网点EOSID（统一的财务单元eosid）
        /// </summary>
        [Display(Name = "网点")]
        [NoExport]
        public int TeachOrgFinaUnitEOSID { get; set; }


        /// <summary>
        /// 来源系统的班级id
        /// </summary>
        [Display(Name = "来源系统的班级id")]
        [NoExport]
        public string ClassID { get; set; }


        /// <summary>
        /// CourseID
        /// </summary>
        [NoExport]
        [Display(Name = "CourseID")]
        public string CourseID { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        [Display(Name = "年份")]
        [NoExport]
        public int ClassYear { get; set; }

        /// <summary>
        /// 来源系统的期段id
        /// </summary>
        [Display(Name = "期段id")]
        [NoExport]
        public string TermID { get; set; }

        /// <summary>
        /// 期段名称
        /// </summary>
        [Display(Name = "期段名称")]
        [NoExport]
        public string TermName { get; set; }


        /// <summary>
        /// MinStudentAmoun
        /// </summary>
        [Display(Name = "MinStudentAmoun")]
        [NoExport]
        public int MinStudentAmoun { get; set; }

        /// <summary>
        /// 班主任id
        /// </summary>
        [Display(Name = "班主任id")]
        [NoExport]
        public string ClassMasterUserID { get; set; }

        /// <summary>
        /// 班主任姓名
        /// </summary>
        [Display(Name = "班主任姓名")]
        [NoExport]
        public string ClassMasterUserName { get; set; }

        /// <summary>
        /// 班级状态(0：无效、1：有效）
        /// </summary>
        [Display(Name = "班级状态")]
        [NoExport]
        public int ClassStatus { get; set; }

        /// <summary>
        /// 业务创建日期
        /// </summary>
        [Display(Name = "业务创建日期")]
        [NoExport]
        public DateTime ProductCreatedDate { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [NoExport]
        public string Describe { get; set; }

        /// <summary>
        /// CourseStartTime
        /// </summary>
        [Display(Name = "CourseStartTime")]
        [NoExport]
        public DateTime? CourseStartTime { get; set; }

        /// <summary>
        /// CourseEndTime
        /// </summary>
        [Display(Name = "CourseEndTime")]
        [NoExport]
        public DateTime? CourseEndTime { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [Display(Name = "版本号")]
        [NoExport]
        public long VersionNum { get; set; } = 0;


        /// <summary>
        /// 所属一级组织id
        /// </summary>
        [NoExport]
        public Guid TeachLevelOneOrgID { get; set; }
    }
}
