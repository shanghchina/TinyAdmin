using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Domain
{
    /// <summary>
    /// 通用常量配置
    /// </summary>
    public class GlobalConstConfig
    {
        /// <summary>
        /// 空Guid字符串00000000-0000-0000-0000-000000000000
        /// </summary>
        public const string GUID_EMPTY = "00000000-0000-0000-0000-000000000000";

        #region 字典表大类Guid
        /// <summary>
        /// 父节点_数据来源-校管家、boss等系统
        /// </summary>
        public const string DICT_PGUID_POC_DATA_SOURCE = "C4AC5D00-0EDC-4B76-95AF-AC1EB1002A0";

        /// <summary>
        /// 父节点_课程所属年级
        /// </summary>
        public const string DICT_PGUID_SHIFT_GRADE = "2E9AED60-7EF3-477D-ABE3-0541F7DA3501";
        /// <summary>
        /// 父节点_课程所属类型
        /// </summary>
        public const string DICT_PGUID_SHIFT_CATEGORY = "D7459004-5E26-43C7-B066-7F93F29D9A34";
        /// <summary>
        /// 父节点_课程所属科目
        /// </summary>
        public const string DICT_PGUID_SHIFT_SUBJECT = "1A930526-9F51-41BC-AC35-C890D0B60918";

        /// <summary>
        /// 父节点_课程所属期段
        /// </summary>
        public const string DICT_PGUID_SHIFT_TERM = "0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5";

        /// <summary>
        /// 父节点_课程所属班型
        /// </summary>
        public const string DICT_PGUID_SHIFT_TYPE = "AA98545B-495F-46FE-AA90-25C418E96C8C";

        #endregion

    }
}
