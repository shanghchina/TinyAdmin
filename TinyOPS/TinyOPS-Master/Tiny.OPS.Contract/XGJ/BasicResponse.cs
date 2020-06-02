using System;

namespace Tiny.OPS.Contract.XGJ
{
    public class BasicResponse
    {
        public Guid id { get; set; }

        public string createtime { get; set; }

        public string updatetime { get; set; }

        public string value { get; set; }

        public string code { get; set; }

        public int issys { get; set; }

        public string describe { get; set; }

        public int status { get; set; }
    }
}
