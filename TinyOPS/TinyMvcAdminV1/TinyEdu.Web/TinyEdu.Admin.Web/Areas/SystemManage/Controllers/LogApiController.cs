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
    public class LogApiController : BaseController
    {
        private LogApiBLL logApiBLL = new LogApiBLL();

        #region 视图功能
        public IActionResult LogApiIndex()
        {
            return View();
        }

        public IActionResult LogApiDetail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        public async Task<IActionResult> GetListJson(LogApiListParam param)
        {
            TData<List<LogApiEntity>> obj = await logApiBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetPageListJson(LogApiListParam param, Pagination pagination)
        {
            TData<List<LogApiEntity>> obj = await logApiBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<LogApiEntity> obj = await logApiBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await logApiBLL.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAllFormJson()
        {
            TData obj = await logApiBLL.RemoveAllForm();
            return Json(obj);
        }
        #endregion
    }
}
