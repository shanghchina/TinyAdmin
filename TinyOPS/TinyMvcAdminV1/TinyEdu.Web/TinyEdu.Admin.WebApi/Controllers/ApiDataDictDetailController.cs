using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyEdu.Business.SystemManage;
using TinyEdu.Entity.SystemManage;
using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Util.Model;

namespace TinyEdu.Admin.WebApi.Controllers
{
    /// <summary>
    /// 字典明细
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiDataDictDetailController : Controller
    {
        private DataDictDetailBLL dataDictDetailBLL = new DataDictDetailBLL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        #region 获取数据
        [HttpGet]
        [ApiAuthorize]
        public async Task<IActionResult> GetListJson(DataDictDetailListParam param)
        {
            TData<List<DataDictDetailEntity>> obj = await dataDictDetailBLL.GetList(param);
            obj.Tag = 1;
            return Json(obj);
        }
        #endregion

        //[HttpGet]
        //[ApiAuthorize]
        //public async Task<IActionResult> GetPageListJson(DataDictDetailListParam param, Pagination pagination)
        //{
        //    TData<List<DataDictDetailEntity>> obj = await dataDictDetailBLL.GetPageList(param, pagination);
        //    return Json(obj);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetFormJson(long id)
        //{
        //    TData<DataDictDetailEntity> obj = await dataDictDetailBLL.GetEntity(id);
        //    return Json(obj);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetMaxSortJson()
        //{
        //    TData<int> obj = await dataDictDetailBLL.GetMaxSort();
        //    return Json(obj);
        //}
        //#endregion

        //#region 提交数据
        //[HttpPost]
        //[ApiAuthorize]
        //public async Task<IActionResult> SaveFormJson(DataDictDetailEntity entity)
        //{
        //    TData<string> obj = await dataDictDetailBLL.SaveForm(entity);
        //    return Json(obj);
        //}

        //[HttpPost]
        //[ApiAuthorize]
        //public async Task<IActionResult> DeleteFormJson(string ids)
        //{
        //    TData obj = await dataDictDetailBLL.DeleteForm(ids);
        //    return Json(obj);
        //}
        //#endregion
    }
}