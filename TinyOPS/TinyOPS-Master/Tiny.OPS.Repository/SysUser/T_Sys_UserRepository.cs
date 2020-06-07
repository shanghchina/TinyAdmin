using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.Repository
{
    /// <summary>
    /// 系统用户
    /// </summary>
	public class T_Sys_UserRepository : RepositoryBase, IT_Sys_UserRepository
    {
        public T_Sys_User GetSysUserByName(string userName)
        {
            string sql = "SELECT * FROM [dbo].[T_Sys_User] where username = @username";
            return GetInfos<T_Sys_User>(sql, new { username = userName }).FirstOrDefault();
        }
    }
}

