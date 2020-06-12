using System;
using System.Collections.Generic;
using TinyEdu.Model.Param.SystemManage;

namespace TinyEdu.Model.Param.OrganizationManage
{
    public class NewsListParam : BaseAreaParam
    {
        public string NewsTitle { get; set; }
        public int? NewsType { get; set; }
        public string NewsTag { get; set; }
    }
}
