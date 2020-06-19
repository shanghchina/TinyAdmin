using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyEdu.Business.SystemManage;
using TinyEdu.Entity.SystemManage;
using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Model.Result;
using TinyEdu.Util.Model;

namespace TinyEdu.Admin.WebApi.Controllers
{
    /// <summary>
    /// 菜单功能api
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiMenuController : Controller
    {
        private MenuBLL sysMenuBLL = new MenuBLL();

        //#region 获取数据
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetListJson(MenuListParam param)
        //{
        //    TData<List<MenuEntity>> obj = await sysMenuBLL.GetList(param);
        //    return Json(obj);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMenuTreeListJson([FromQuery]MenuListParam param)
        {
            TData<List<ZtreeInfo>> obj = await sysMenuBLL.GetZtreeList(param);
            return Json(obj);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetFormJson(long id)
        //{
        //    TData<MenuEntity> obj = await sysMenuBLL.GetEntity(id);
        //    return Json(obj);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="parentId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetMaxSortJson(long parentId = 0)
        //{
        //    TData<int> obj = await sysMenuBLL.GetMaxSort(parentId);
        //    return Json(obj);
        //}
        //#endregion

        //#region 提交数据
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> SaveFormJson(MenuEntity entity)
        //{
        //    TData<string> obj = await sysMenuBLL.SaveForm(entity);
        //    return Json(obj);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> DeleteFormJson(string ids)
        //{
        //    TData obj = await sysMenuBLL.DeleteForm(ids);
        //    return Json(obj);
        //}
        //#endregion
    }
}