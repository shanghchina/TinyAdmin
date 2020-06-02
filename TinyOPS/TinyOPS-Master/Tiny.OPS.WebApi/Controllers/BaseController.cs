using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tiny.OPS.Contract;

namespace Tiny.OPS.WebApi.Controllers
{
    /// <summary>
    /// 控制器基础类
    /// </summary>
    public class BaseController : ControllerBase
    {


        /// <summary>
        /// API规范返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected BaseResponseModel<T> ApiSuccessResult<T>(T data)
        {
            return new BaseResponseModel<T>() { Code = "0", Message = "操作成功", Data = data };
        }
        /// <summary>
        /// API规范返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        protected BaseResponseModel<string> ApiErrorResult(string message)
        {
            return new BaseResponseModel<string>() { Code = "-1", Message = message, Data = "" };
        }

        /// <summary>
        /// API规范返回值
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected BaseResponseModel<string> ApiFailResult(string message)
        {
            return new BaseResponseModel<string>() { Code = "999", Message = message, Data = "" };
        }

    }
}