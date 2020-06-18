using System.ComponentModel.DataAnnotations;

namespace TinyEdu.Common.Dapper.Enumeration
{

    public enum EumDBWay
    {
        /// <summary>
        /// 读
        /// </summary>
        [Display(Name = "Read")]
        Reader = 10,
        /// <summary>
        /// 写
        /// </summary>
        [Display(Name = "Write")]
        Writer = 20,
       
    }
}