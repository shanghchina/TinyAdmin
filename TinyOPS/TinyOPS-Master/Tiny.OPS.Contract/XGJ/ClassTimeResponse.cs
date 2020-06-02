using System;

namespace Tiny.OPS.Contract.XGJ
{
    public class ClassTimeResponse
    {
        public Guid ClassID { get; set; }
        public int WeekDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Guid ClassroomID { get; set; }
        public string ClassroomName { get; set; }
        public Guid? TeacherUserID { get; set; }
        public string TeacherName { get; set; }
        public Guid? AngLiTeacherID { get; set; }
    }
}
