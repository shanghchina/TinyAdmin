using System.Collections.Generic;

namespace Tiny.OPS.Common.Web.XGJTools
{
    public class PushModel
    {
        public string Name { get; set; }
        public string Root { get; set; }
        public List<PushSystem> Systems { get; set; } = new List<PushSystem>();
    }

    public class PushSystem
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 系统编码
        /// </summary>
        public string Value { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// 事业部系统配置
        /// </summary>
        public List<PushConfig> Configs { get; set; } = new List<PushConfig>();
    }

    public class PushConfig
    {
        /// <summary>
        /// 来源系统
        /// </summary>
        public int FromSystem { get; set; }
        /// <summary>
        /// 一级招生组织事业部Guid
        /// </summary>
        public string ChargeLevelOneOrgId { get; set; }

        /// <summary>
        /// 一级招生组织事业部
        /// </summary>
        public string ChargeLevelOneOrgName { get; set; }
        /// <summary>
        /// 事业部名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 事业部guid
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// AppID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// Secret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// Token url
        /// </summary>
        public string TokenUrl { get; set; }
        /// <summary>
        /// 异常url
        /// </summary>
        public string ErrorUrl { get; set; }
        /// <summary>
        /// 产品中心来源系统
        /// </summary>
        public int POCSource { get; set; }
        /// <summary>
        /// 是否缓存Token
        /// </summary>
        public string EnableTokenCache { get; set; }
    }
}
