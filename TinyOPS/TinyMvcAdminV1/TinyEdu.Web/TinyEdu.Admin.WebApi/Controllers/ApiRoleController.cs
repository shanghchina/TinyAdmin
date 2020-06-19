using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyEdu.Business.SystemManage;
using TinyEdu.Entity.SystemManage;
using TinyEdu.Model;
using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Util.Model;

namespace TinyEdu.Admin.WebApi.Controllers
{
    /// <summary>
    /// 角色管理api接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiRoleController : Controller
    {
        private RoleBLL sysRoleBLL = new RoleBLL();

        #region 获取数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListJson(RoleListParam param)
        {
            TData<List<RoleEntity>> obj = await sysRoleBLL.GetList(param);
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetPageListJson(RoleListRequest paramRequest)
        {
            RoleListParam param = paramRequest.param;
            Pagination pagination = paramRequest.pagination;
            TData<List<RoleEntity>> obj = await sysRoleBLL.GetPageList(param, pagination);
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
            TData<RoleEntity> obj = await sysRoleBLL.GetEntity(id);
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoleName(RoleListParam param)
        {
            TData<string> obj = new TData<string>();
            var list = await sysRoleBLL.GetList(param);
            if (list.Tag == 1)
            {
                obj.Result = string.Join(",", list.Result.Select(p => p.RoleName));
                obj.Tag = 1;
            }
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await sysRoleBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveFormJson(RoleEntity entity)
        {
            TData<string> obj = await sysRoleBLL.SaveForm(entity);
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
            TData obj = await sysRoleBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion

    }
}