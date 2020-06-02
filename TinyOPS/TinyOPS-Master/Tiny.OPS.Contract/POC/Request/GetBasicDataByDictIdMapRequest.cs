using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Contract
{
    public class GetBasicDataByDictIdMapRequest
    {
        /// <summary>
        /// 来源系统
        /// </summary>
        public List<int> pocSource { get; set; }
       /// <summary>
       /// 字典Guid
       /// </summary>
        public List<Guid> fkDictGuid { get; set; }
    }
}
