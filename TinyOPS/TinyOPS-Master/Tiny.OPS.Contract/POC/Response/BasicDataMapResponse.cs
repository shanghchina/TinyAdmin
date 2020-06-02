using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 基础参数响应类
    /// </summary>
    public class BasicDataMapResponse
    {
        /// <summary>
        /// 基础参数集合
        /// </summary>
        public List<BasicDataMap> DataList { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }

    /// <summary>
    /// 基础数据列表实体
    /// </summary>
    public class BasicDataMap
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 基础数据GUID
        /// </summary>
        public Guid BasicDataGuid { get; set; }

        /// <summary>
        /// 系统来源
        /// </summary>
        public string FromSystem { get; set; }
 
        /// <summary>
        /// 事业部名称
        /// </summary>
        public string OneOrgName { get; set; }
        /// <summary>
        /// 参数类型名称
        /// </summary>
        public string DictTypeName { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public string DictValue { get; set; }
        /// <summary>
        /// 是否映射(0是未映射，1是映射)
        /// </summary>
        public string IsMapping { get; set; }
        /// <summary>
        /// 创建时间，格式yyyyMMddHHmmss
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// 映射时间，格式yyyyMMddHHmmss
        /// </summary>
        public string UpdateDate { get; set; }
        /// <summary>
        /// 基础参数映射Id
        /// </summary>
        public Guid BasicDataMapGuid { get; set; }
        /// <summary>
        /// 映射的基础参数子级
        /// </summary>
        public Guid FKDictGuid { get; set; }
        /// <summary>
        /// 分类显示名称（名称加guid），eg:课程所属年级>一年级|guid1,guid2
        /// </summary>
        public string SysCatalogTitle { get; set; }
    }
}
