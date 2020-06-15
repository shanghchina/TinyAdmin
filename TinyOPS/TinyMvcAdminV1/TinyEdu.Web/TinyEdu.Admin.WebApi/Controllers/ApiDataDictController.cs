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
    public class ApiDataDictController : Controller
    {
        private DataDictBLL dataDictBLL = new DataDictBLL();

        #region 获取数据
        //[HttpGet]
        //public async Task<IActionResult> GetListJson(DataDictListParam param)
        //{
        //    TData<List<DataDictEntity>> obj = await dataDictBLL.GetList(param);
        //    return Json(obj);
        //}

        /// <summary>
        /// GetPageListJson
        /// </summary>
        /// <param name="paramRequest"></param>
        /// <returns></returns>
        [ApiAuthorize]
        [HttpPost]
        public async Task<IActionResult> GetPageListJson(DataDictListRequest paramRequest)
        {
            DataDictListParam param = paramRequest.param;
            Pagination pagination = paramRequest.pagination;
            TData<List<DataDictEntity>> obj = await dataDictBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        /// <summary>
        /// 字典类型界面信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiAuthorize]
        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<DataDictEntity> obj = await dataDictBLL.GetEntity(id);
            return Json(obj);
        }

        /// <summary>
        /// 获取最大序号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await dataDictBLL.GetMaxSort();
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDataDictListJson()
        {
            TData<List<DataDictInfo>> obj = await dataDictBLL.GetDataDictList();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存字典类型
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveFormJson(DataDictEntity entity)
        {
            TData<string> obj = await dataDictBLL.SaveForm(entity);
            return Json(obj);
        }

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await dataDictBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
