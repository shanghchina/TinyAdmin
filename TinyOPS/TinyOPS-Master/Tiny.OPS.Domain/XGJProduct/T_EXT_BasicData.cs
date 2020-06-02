using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Data;
using System;

namespace Tiny.OPS.Domain.XGJProduct
{
    [Table("T_EXT_BasicData", DBName = EumDBName.POC)]
    public class T_EXT_BasicData : EntityBase
    {
        /// <summary>
        /// 基础数据GUID
        /// </summary>
        public Guid BasicDataGuid { get; set; }

        /// <summary>
        /// 系统来源
        /// </summary>
        public int FromSystem { get; set; }

        /// <summary>
        /// 事业部guid
        /// </summary>
        public string OneOrgId { get; set; }

        /// <summary>
        /// 事业部名称
        /// </summary>
        public string OneOrgName { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string DictTypeCode { get; set; }

        /// <summary>
        /// 参数类型名称
        /// </summary>
        public string DictTypeName { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int DictSort { get; set; }

        /// <summary>
        /// 字典id
        /// </summary>
        public string DictId { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 创建时间，格式yyyyMMddHHmmss
        /// </summary>
        public DateTime DictCreatetime { get; set; }

        /// <summary>
        /// 更新时间，格式yyyyMMddHHmmss
        /// </summary>
        public DateTime DictUpdatetime { get; set; }

        /// <summary>
        /// 内部代码（可忽略）
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 是否为系统内置项（不允许修改）（0否，1是）
        /// </summary>
        public int DictIsSys { get; set; }

        /// <summary>
        /// 字典状态（0无效，1有效）
        /// </summary>
        public int DictStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string DictDescribe { get; set; }

        /// <summary>
        /// 修改人Id
        /// </summary>
        public string UpdaterUserId { get; set; }

        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string UpdaterUserName { get; set; }

        /// <summary>
        /// 产品中心来源系统
        /// </summary>
        public int POCSource { get; set; }

        /// <summary>
        ///DictGuid 
        /// </summary>
        [NoMapper]
         public Guid FKDictGuid { get; set; }

        /// <summary>
        /// 来源系统
        /// </summary>
        [NoMapper]
        public string PocSourceName { get; set; }
    }
}
