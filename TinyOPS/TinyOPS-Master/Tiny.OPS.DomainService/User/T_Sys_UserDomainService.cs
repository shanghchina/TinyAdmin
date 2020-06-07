
using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;

namespace Tiny.OPS.DomainService
{
    public class T_Sys_UserDomainService : RealDomainService<T_Sys_User>, IT_Sys_UserDomainService
    {

        IT_Sys_UserRepository iT_Sys_UserRepository = IoC.Resolve<IT_Sys_UserRepository>();

        public Vm_Sys_User GetSysUserByName(string userName)
        {
            var entity = iT_Sys_UserRepository.GetSysUserByName(userName);
            var result = new Vm_Sys_User()
            {
                Id = entity.Id, //	Id	
                //tb_guid = entity.tb_guid,   //	表Guid	
                avatar_id = entity.avatar_id,   //	头像id	
                email = entity.email,   //	邮箱	
                is_admin = entity.is_admin, //	管理员	
                is_enabled = entity.is_enabled, //	状态：1启用、0禁用	
                password = entity.password, //	密码	
                username = entity.username, //	用户名	
                dept_id = entity.dept_id,   //	部门名称id	
                phone = entity.phone,   //	手机号码	
                //post_id = entity.post_id,   //	岗位名称	
                //last_password_reset_time = entity.last_password_reset_time, //	最后修改密码的日期	
                nick_name = entity.nick_name,   //	昵称	
                sex = entity.sex   //	性别	
            };

            return result;

        }
    }
}

