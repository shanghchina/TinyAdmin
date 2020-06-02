using System;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 字典表viewModel
    /// </summary>
    public class VM_SYS_Dictionary : VMEntityBase
    {
        /// <summary>
        /// 系统来源例如从学员中心过来数据
        /// </summary>
        public int FromSystem { get; set; }

        /// <summary>
        /// 如果为空表示全局使用
        /// </summary>
        public Guid OneOrgId { get; set; }

        /// <summary>
        /// OneOrgName
        /// </summary>
        public string OneOrgName { get; set; }

        /// <summary>
        /// guid Key
        /// </summary>
        public Guid DictGuid { get; set; }

        /// <summary>
        /// 父guid
        /// </summary>
        public Guid ParentGuid { get; set; }
        /// <summary>
        /// 名称或分类名称
        /// </summary>
        public string SysDictKey { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string SysDictValue { get; set; }
        /// <summary>
        /// DictDesc
        /// </summary>
        public string SysDictDesc { get; set; }
        /// <summary>
        /// 顺序号，默认为0
        /// </summary>
        public int SysDictSort { get; set; }
        /// <summary>
        /// 系统分类代码-用于父类型
        /// </summary>
        public string SysCatalogCode { get; set; }

        /// <summary>
        /// 系统分类代码名称
        /// </summary>
        public string SysCatalogName { get; set; }

        /// <summary>
        /// 是否是分类1是0否
        /// </summary>
        public bool SysIsCatalog { get; set; }

        
        /// <summary>
        /// IsEnabled
        /// </summary>
        public bool SysIsEnabled { get; set; }
        /// <summary>
        /// 是否编辑默认1可编辑 0不可编辑 
        /// </summary>
        public bool SysIsEdit { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdaterUserId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdaterUserName { get; set; }

        /// <summary>
        /// 该父节点包含的子节点显示的名称
        /// </summary>
        public string ChinldSysDictValueLable { get; set; }

    }

}


