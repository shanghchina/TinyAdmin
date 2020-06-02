using Dapper;
using Tiny.Common.Dapper.Entity;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.Common.Types;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Tiny.OPS.Repository
{
    public class UserRoleRangeRepository : RepositoryBase, IUserRoleRangeRepository
    {
        public bool RemoveByUserId(long userId)
        {
            var _agentresult = ExecuteCommand<T_RLS_UserRoleRange>(EumDBWay.Writer, EumDBName.POC.GetDisplayName(),"delete from T_RLS_UserRoleRange where UserID=@UserID ", new { UserID = userId });
            if (_agentresult.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
