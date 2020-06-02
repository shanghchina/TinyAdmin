using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Domain.Enum
{
    public enum SynchroDataType
    {
        ProductTypeList = 1000,
        ShiftList = 2000,
        ClassList = 3000,
        BaseData = 4000
    }

    public enum SynchroStatus
    {
        Success = 1000,
        Failure = 2000,
        ManualSuccess = 3000, //手动获取成功，区别于windows服务跑的1000的成功
        ManualFailure = 4000, //手动获取失败，区别于windows服务跑的2000的失败
    }

    /// <summary>
    /// 校管家接口配置状态
    /// </summary>
    public enum XGJInterfaceConfigureStatus
    {
        /// <summary>
        /// 产品启用
        /// </summary>
        ProductEnable = 1000,
        /// <summary>
        /// 基础数据启用
        /// </summary>
        BasicDataEnable = 2000,
        /// <summary>
        /// 禁用
        /// </summary>
        Forbidden = 7000,
    }
}
