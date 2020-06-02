﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlyEdu.Common.Dapper.Validate
{
    public class RuleInfo
    {
        /// <summary>
        /// 规则
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// 验证类型
        /// </summary>
        public IList<ValidationType> ValidationTypes { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否多行
        /// </summary>
        public bool IsMultiline { get; set; }
        /// <summary>
        /// 是否忽略大小写
        /// </summary>
        public bool IsIgnoreCase { get; set; }
        /// <summary>
        /// 是否为范围验证
        /// </summary>
        public bool IsRange { get; set; }

    }
}
