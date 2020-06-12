using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyEdu.Business.SystemManage;
using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Model.Result.SystemManage;
using TinyEdu.Util.Model;

namespace TinyEdu.Admin.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class DataDictController : ControllerBase
    {
        private DataDictBLL dataDictBLL = new DataDictBLL();

        #region 获取数据
        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TData<List<DataDictInfo>>> GetList([FromQuery]DataDictListParam param)
        {
            TData<List<DataDictInfo>> obj = await dataDictBLL.GetDataDictList();
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}