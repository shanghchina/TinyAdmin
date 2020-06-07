using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    /// <summary>
    /// 系统令牌
    /// </summary>
	public interface IT_Sys_UserRepository : IRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        T_Sys_User GetSysUserByName(string userName);
    }
}

