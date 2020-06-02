using System;
using System.Collections.Generic;

namespace Tiny.OPS.Contract.XGJ
{
    public class ClassResponse
    {
        public Guid ID { get; set; }
        public Guid OrgID { get; set; }
        public string OrgName { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public Guid CampusID { get; set; }
        public string CampusName { get; set; }
        public Guid AngLiCampusID { get; set; }
        public string EOSID { get; set; }
        public int Year { get; set; }
        public Guid TermID { get; set; }
        public string TermName { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int MaxStudentAmount { get; set; }
        public decimal CourseTimes { get; set; }
        public string UnitName { get; set; }
        public Guid MasterUserID { get; set; }
        public string MasterUserName { get; set; }
        public Guid AngLiMasterID { get; set; }
        public Guid ShiftID { get; set; }
        public string ShiftName { get; set; }
        public int Status { get; set; }
        public string Describe { get; set; }
        public DateTime? CourseStartTime { get; set; }
        public DateTime? CourseEndTime { get; set; }
        public List<ClassTimeResponse> ClassTimeList { get; set; } = new List<ClassTimeResponse>();
    }
}
