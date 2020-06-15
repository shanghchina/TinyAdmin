using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyEdu.Business.SystemManage;
using TinyEdu.Entity.SystemManage;
using TinyEdu.Model;
using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Model.Result.SystemManage;
using TinyEdu.Util.Model;

namespace TinyEdu.Admin.WebApi.Controllers
{
    /// <summary>
    /// DataDictController
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[AuthorizeFilter]
    public class DataDictApiController : ControllerBase
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

        ///// <summary>
        ///// GetPageListJson
        ///// </summary>
        ///// <param name="paramRequest"></param>
        ///// <returns></returns>
        //[HttpGet]
        ////[AuthorizeFilter("system:datadict:search")]
        //public async Task<IActionResult> GetPageListJson(DataDictListRequest paramRequest)
        //{
        //    DataDictListParam param = new DataDictListParam();
        //    param.DictType = paramRequest.DictType;
        //    param.Remark = paramRequest.Remark;

        //    Pagination pagination = paramRequest.pagination;
        //    TData<List<DataDictEntity>> obj = await dataDictBLL.GetPageList(param, pagination);
        //    obj.Tag = 1;
        //    return Json(obj);
        //}

        /// <summary>
        /// GetPageListJson
        /// </summary>
        /// <param name="paramRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthorize]
        public async Task<TData<List<DataDictEntity>>> GetPageListJson(DataDictListRequest paramRequest)
        {
            DataDictListParam param = new DataDictListParam();
            param.DictType = paramRequest.DictType;
            param.Remark = paramRequest.Remark;

            Pagination pagination = paramRequest.pagination;
            TData<List<DataDictEntity>> obj = await dataDictBLL.GetPageList(param, pagination);
            obj.Tag = 1;
            return obj;
        }

        ///// <summary>
        ///// GetPageListJson
        ///// </summary>
        ///// <param name="paramRequest"></param>
        ///// <returns></returns>
        //[HttpPost]
        ////[AuthorizeFilter("system:datadict:search")]
        //public async Task<IActionResult> GetPageListJson([FromBody]DataDictListRequest paramRequest)
        //{
        //    DataDictListParam param = new DataDictListParam();
        //    param.DictType = paramRequest.DictType;
        //    param.Remark = paramRequest.Remark;

        //    Pagination pagination = paramRequest.pagination;
        //    TData<List<DataDictEntity>> obj = await dataDictBLL.GetPageList(param, pagination);
        //    obj.Tag = 1;
        //    return Json(obj);
        //}
    }
}
