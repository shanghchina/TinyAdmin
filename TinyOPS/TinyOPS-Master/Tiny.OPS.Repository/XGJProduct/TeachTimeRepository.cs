using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.Repository
{
    public class TeachTimeRepository : RepositoryBase, ITeachTimeRepository
    {
        public void DeleteTeachTime(long classID)
        {
            var _result = ExecuteScalar<T_EXT_ClassTeachTime>("delete from [T_EXT_ClassTeachTime] where [ProductClassID] = @classID", new { classID = @classID });
        }
    }
}
