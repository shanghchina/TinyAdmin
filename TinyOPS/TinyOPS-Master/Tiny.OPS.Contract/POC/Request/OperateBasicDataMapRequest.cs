using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 基础参数映射操作
    /// </summary>
    public class OperateBasicDataMapRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 基础数据映射表Guid("00000000-0000-0000-0000-000000000000"为新增，否则为修改)
        /// </summary>
        public Guid BasicDataMapGuid { get; set; }
        /// <summary>
        /// 基础数据映来源表Guid
        /// </summary>
        public Guid FKBasicDataGuid { get; set; }
        /// <summary>
        /// POC系统基础数据表Guid(在字典表中)
        /// </summary>
        public Guid FKDictGuid { get; set; }
        /// <summary>
        /// 是否是分类(基础数据默认分类)
        /// </summary>
        public bool SysIsCatalog => true;
        /// <summary>
        /// 分类显示名称(名称A>名称B|名称A的Guid.名称B的Guid)
        /// </summary>
        public string SysCatalogTitle { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        public string UpdaterUserId { get; set; }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string UpdaterUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
