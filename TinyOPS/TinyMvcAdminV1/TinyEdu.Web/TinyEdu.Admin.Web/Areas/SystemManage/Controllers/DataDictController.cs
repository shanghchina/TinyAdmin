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
using TinyEdu.Model.Result.SystemManage;

namespace TinyEdu.Admin.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class DataDictController : BaseController
    {
        private DataDictBLL dataDictBLL = new DataDictBLL();

        #region 视图功能
        [AuthorizeFilter("system:datadict:view")]
        public IActionResult DataDictIndex()
        {
            return View();
        }

        public IActionResult DataDictForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("system:datadict:search")]
        public async Task<IActionResult> GetListJson(DataDictListParam param)
        {
            TData<List<DataDictEntity>> obj = await dataDictBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("system:datadict:search")]
        public async Task<IActionResult> GetPageListJson(DataDictListParam param, Pagination pagination)
        {
            TData<List<DataDictEntity>> obj = await dataDictBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<DataDictEntity> obj = await dataDictBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await dataDictBLL.GetMaxSort();
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetDataDictListJson()
        {
            TData<List<DataDictInfo>> obj = await dataDictBLL.GetDataDictList();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("system:datadict:add,system:datadict:edit")]
        public async Task<IActionResult> SaveFormJson(DataDictEntity entity)
        {
            TData<string> obj = await dataDictBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("system:datadict:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await dataDictBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
