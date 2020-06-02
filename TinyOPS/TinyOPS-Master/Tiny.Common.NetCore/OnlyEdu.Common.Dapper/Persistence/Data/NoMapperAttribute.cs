using System;
using System.Collections.Generic;
using System.Text;

namespace OnlyEdu.Common.Dapper.Persistence.Data
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoMapperAttribute : Attribute
    {
    }
}
