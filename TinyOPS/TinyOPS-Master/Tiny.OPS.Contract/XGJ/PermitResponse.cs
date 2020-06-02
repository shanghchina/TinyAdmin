using System;

namespace Tiny.OPS.Contract.XGJ
{
    public class PermitResponse
    {
        public Guid ShiftID { get; set; }
        public Guid CampusID { get; set; }
        public string CampusName { get; set; }
        public Guid AngLiCampusID { get; set; }
        public string ESOID { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
