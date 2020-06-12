﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyEdu.Util;
using TinyEdu.Util.Model;
using TinyEdu.Entity;
using TinyEdu.Model;
using TinyEdu.Admin.Web.Controllers;
using TinyEdu.Entity.SystemManage;
using TinyEdu.Business.SystemManage;
using TinyEdu.Model.Param.SystemManage;

namespace TinyEdu.Admin.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class AutoJobLogController : BaseController
    {
        private AutoJobLogBLL autoJobLogBLL = new AutoJobLogBLL();

        #region 视图功能
        [AuthorizeFilter("system:autojob:logview")]
        public IActionResult AutoJobLogIndex()
        {
            return View();
        }
        public IActionResult AutoJobLogForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        public async Task<IActionResult> GetListJson(AutoJobLogListParam param)
        {
            TData<List<AutoJobLogEntity>> obj = await autoJobLogBLL.GetList(param);
            return Json(obj);
        }
        [HttpGet]
        public async Task<IActionResult> GetPageListJson(AutoJobLogListParam param, Pagination pagination)
        {
            TData<List<AutoJobLogEntity>> obj = await autoJobLogBLL.GetPageList(param, pagination);
            return Json(obj);
        }
        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<AutoJobLogEntity> obj = await autoJobLogBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        public async Task<IActionResult> SaveFormJson(AutoJobLogEntity entity)
        {
            TData<string> obj = await autoJobLogBLL.SaveForm(entity);
            return Json(obj);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await autoJobLogBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
