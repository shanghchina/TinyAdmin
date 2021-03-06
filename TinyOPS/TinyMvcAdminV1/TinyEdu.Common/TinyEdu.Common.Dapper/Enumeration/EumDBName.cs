﻿using System.ComponentModel.DataAnnotations;

namespace TinyEdu.Common.Dapper.Enumeration
{
    public enum EumDBName
    {
        /// <summary>
        /// POC
        /// </summary>
        [Display(Name = "POC")]
        POC = 10,

        /// <summary>
        /// TSC
        /// </summary>
        [Display(Name = "TSC")]
        TSC = 20,

    }
}