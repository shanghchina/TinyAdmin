using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyEdu.Business.SystemManage;
using TinyEdu.Entity.SystemManage;
using TinyEdu.Model;
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
        public async Task<IActionResult> GetListJson(DataDictDetailListParam param)
        {
            TData<List<DataDictDetailEntity>> obj = await dataDictDetailBLL.GetList(param);
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetPageListJson(DataDictDetailListRequest paramRequest)
        {
            DataDictDetailListParam param = paramRequest.param;
            Pagination pagination = paramRequest.pagination;
            TData<List<DataDictDetailEntity>> obj = await dataDictDetailBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<DataDictDetailEntity> obj = await dataDictDetailBLL.GetEntity(id);
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await dataDictDetailBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        #region 提交数据
        [HttpPost]
        public async Task<IActionResult> SaveFormJson(DataDictDetailEntity entity)
        {
            TData<string> obj = await dataDictDetailBLL.SaveForm(entity);
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await dataDictDetailBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}