using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;


namespace Tiny.OPS.DomainService
{
    /// <summary>
    /// 系统用户
    /// </summary>
	public interface IT_Sys_UserDomainService : IDomainService
    {
        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Vm_Sys_User GetSysUserByName(string userName);
    }
}

