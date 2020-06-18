using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyEdu.Admin.Contract;
using TinyEdu.Admin.DomainService;
using TinyEdu.Common.Dapper.DI;

namespace TinyEdu.Admin.WebApi.Controllers
{
    /// <summary>
    /// 心跳检测
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LivelyController : ApiBaseController
    {
        private IHostingEnvironment _hostingEnvironment = null;
        private IAPP_SysLogDomainService app_SysLogDomainService => IoC.Resolve<IAPP_SysLogDomainService>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public LivelyController(IHostingEnvironment host)
        {
            this._hostingEnvironment = host;
        }

        // GET: Lively/Test
        /// <summary>
        /// 活跃接口功能Test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NoCheck]
        public BaseResponse Test()
        {
            try
            {
                var result = ReadWriteFileAndDB();
                return ApiSuccessResult(result);
            }
            catch (Exception ex)
            {
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 读写文件和数据库
        /// </summary>
        [HttpGet]
        private Lively ReadWriteFileAndDB()
        {
            Lively result = new Lively();
            try
            {
                string webRootPath = _hostingEnvironment.ContentRootPath;
                string fileName = webRootPath + @"\LivelyTest.txt";

                result.Operation = "写入文件";
                string strWrite = "活跃接口功能写入测试";
                System.IO.File.WriteAllText(fileName, strWrite);
                result.OperationResult = "写入文件成功";

                result.Operation = result.Operation + "-->" + "读取文件";
                string str = System.IO.File.ReadAllText(fileName);
                result.OperationResult = result.OperationResult + "-->" + "读取文件成功";

                //读写数据库
                string logDesc = "ReadWriteDB";
                string logInfo = "读写数据库测试";
                result.Operation = result.Operation + "-->" + "读写数据库测试";
                app_SysLogDomainService.AddLogInfo(logDesc, logInfo);
                result.OperationResult = result.OperationResult + "-->" + "读写数据库测试成功";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }

    /// <summary>
    /// 接口功能定义的类
    /// </summary>
    class Lively
    {
        /// <summary>
        /// 操作内容：读写文件 读写数据库
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 操作结果：成功或报错
        /// </summary>
        public string OperationResult { get; set; }

    }
}
