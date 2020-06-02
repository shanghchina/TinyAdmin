using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OnlyEdu.Common.Types
{
    /// <summary>
    /// 2016-07-08 modify
    /// 应用域
    /// </summary>
    public enum DomainType
    {
        /// <summary>
        /// 管理系统
        /// </summary>
        [Description("管理系统")]
        EMS = 1000,

        /// <summary>
        /// 人事系统
        /// </summary>
        [Description("人事系统")]
        HR = 2000,

        /// <summary>
        /// 项目计划
        /// </summary>
        [Description("项目计划")]
        PPS = 3000,

        /// <summary>
        /// 财务系统
        /// </summary>
        [Description("财务系统")]
        EFS = 4000,

        /// <summary>
        /// 工作流系统
        /// </summary>
        [Description("工作流系统")]
        Workflow = 5000,

        /// <summary>
        /// 授权管理系统
        /// </summary>
        [Description("授权管理系统")]
        RLS = 6000,

        /// <summary>
        /// 数据中心系统
        /// </summary>
        [Description("数据中心")]
        DCS = 7000,

        /// <summary>
        /// 昂立门户
        /// </summary>
        [Description("昂立门户")]
        OnlyPortal = 8000,

        /// <summary>
        /// 报表中心
        /// </summary>
        [Description("报表中心")]
        RPT = 9000,

        /// <summary>
        /// 运营系统
        /// </summary>
        [Description("运营系统")]
        EOS = 9011,

        [Description("收入中心")]
        ECS = 9021,
        [Description("师资中心")]
        TSC = 9031,
        [Description("学员中心")]
        OSC = 9041,
        /// <summary>
        /// 行政管理系统
        /// </summary>
        [Description("行政管理系统")]
        AMS = 10000,

        /// <summary>
        /// 运维管理系统
        /// </summary>
        [Description("运维管理系统")]
        OAM = 11000,

        /// <summary>
        /// 运营平台解决方案
        /// </summary>
        [Description("运营平台解决方案")]
        OPS = 9044,

        /// <summary>
        /// 发票中心
        /// </summary>
        [Description("事务管理系统")]
        ETS = 12000,
        /// <summary>
        /// 发票中心
        /// </summary>
        [Description("发票中心")]
        INV = 13000,
        /// <summary>
        /// 昂立运营工作台
        /// </summary>
        [Description("昂立运营工作台")]
        CWS = 14000,
        /// <summary>
        /// 伙伴
        /// </summary>
        [Description("伙伴")]
        Partner = 991000,
    }


    //
    // 摘要:
    //     2016-05-11 modify 权限授予状态
    public enum AuthorizationStateType
    {
        //
        // 摘要:
        //     授予
        Authorization = 1000,
        //
        // 摘要:
        //     拒绝
        Reject = 2000,
        //
        // 摘要:
        //     移除权限，只针对非岗位权限
        Delete = 3000
    }
    //
    // 摘要:
    //     行状态
    public enum RowStateType
    {
        //
        // 摘要:
        //     有效
        Effectivity = 1,
        //
        // 摘要:
        //     无效
        Expiry = 2
    }
    //
    // 摘要:
    //     HDF 2014-11-28 角色来源
    public enum RoleSourceType
    {
        //
        // 摘要:
        //     职位 岗位范围
        PositionTemplate = 1000,
        //
        // 摘要:
        //     职位 岗位范围变更
        PositionTemplate_Change = 1001,
        //
        // 摘要:
        //     兼任岗位
        UserPartTime = 2000,
        //
        // 摘要:
        //     兼任岗位变更
        UserPartTime_Change = 2001,
        //
        // 摘要:
        //     角色组权限
        RoleGroupAuthority = 3000,
        //
        // 摘要:
        //     角色权限
        RoleAuthority = 4000
    }
    //
    // 摘要:
    //     范围类型
    public enum RoleRangeType
    {
        //
        // 摘要:
        //     人事组织
        Org = 1001,
        //
        // 摘要:
        //     EOS人事组织
        ORG_EOS = 1011,
        //
        // 摘要:
        //     财务组织
        Finance = 2001,
        //
        // 摘要:
        //     DCS档案分类
        DCS_FileCategory = 3001,
        //
        // 摘要:
        //     财务单无范围
        FUnit = 8001
    }

    //
    // 摘要:
    //     来源系统
    public enum SystemType
    {
        /// <summary>
        /// 校管家
        /// </summary>
        XGJ = 1000,

        /// <summary>
        /// EOS
        /// </summary>
        EOS = 2000
    }
}
