using System;

namespace Tiny.OPS.Contract.XGJ
{
    public class ProductTypeResponse
    {
        public Guid ID { get; set; }
        public Guid OrgID { get; set; }
        public string OrgName { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Status { get; set; }
        public string Describe { get; set; }
        public Guid ParentID { get; set; }
    }
}
