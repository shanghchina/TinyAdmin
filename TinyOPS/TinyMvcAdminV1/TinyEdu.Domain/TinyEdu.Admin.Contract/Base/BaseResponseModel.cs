using System;
using System.Collections.Generic;
using System.Text;

namespace TinyEdu.Admin.Contract
{
    /// <summary>
    /// 响应实体基类
    /// </summary>
    public class BaseResponseModel<T> : BaseResponse 
    {
        /// <summary>
        /// 响应业务数据
        /// </summary>
        public T Data { get; set; }

    }

    public class BaseResponse
    {
        /// <summary>
        /// 响应码（0000-成功,9999-失败,0001-参数有误,0002-业务逻辑错误,0003-会话过期,0004-危险数据,0005-系统异常）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; }

    }


}

