using System;

namespace Tiny.OPS.Contract
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class Vm_Sys_User
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }    //	Id

        /// <summary>
        /// 表Guid
        /// </summary>
        public string tb_guid { get; set; } //	表Guid

        /// <summary>
        /// 头像id
        /// </summary>
        public long? avatar_id { get; set; }    //	头像id

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }   //	邮箱

        /// <summary>
        /// 管理员
        /// </summary>
        public bool is_admin { get; set; }  //	管理员

        /// <summary>
        /// 状态：1启用、0禁用
        /// </summary>
        public int is_enabled { get; set; } //	状态：1启用、0禁用

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }    //	密码

        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }    //	用户名

        /// <summary>
        /// 部门名称id
        /// </summary>
        public long? dept_id { get; set; }  //	部门名称id

        /// <summary>
        /// 手机号码
        /// </summary>
        public string phone { get; set; }   //	手机号码

        /// <summary>
        /// 岗位名称
        /// </summary>
        public long post_id { get; set; }   //	岗位名称

        /// <summary>
        /// 最后修改密码的日期
        /// </summary>
        public DateTime last_password_reset_time { get; set; }  //	最后修改密码的日期

        /// <summary>
        /// 昵称
        /// </summary>
        public string nick_name { get; set; }   //	昵称

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }	//	性别
    }

}
