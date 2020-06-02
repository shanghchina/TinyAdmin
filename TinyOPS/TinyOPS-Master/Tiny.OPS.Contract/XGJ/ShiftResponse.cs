using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract.XGJ
{
    public class ShiftResponse
    {
        public Guid ID { get; set; }
        public Guid OrgID { get; set; }
        public string OrgName { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Flag { get; set; }
        public string FlagName { get; set; }
        public string ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public int Year { get; set; }
        public Guid TermID { get; set; }
        public string TermName { get; set; }
        public Guid GradeID { get; set; }
        public string GradeName { get; set; }
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Guid SubjectID { get; set; }
        public string SubjectName { get; set; }
        public Guid ClassType { get; set; }
        public string ClassTypeName { get; set; }
        public decimal CourseTimes { get; set; }
        public string UnitName { get; set; }
        public decimal UnitPrice { get; set; }
        public string UnitPriceName { get; set; }
        public int Status { get; set; }
        public string Describe { get; set; }
        public List<PermitResponse> PermitList { get; set; } = new List<PermitResponse>();
    }
}
