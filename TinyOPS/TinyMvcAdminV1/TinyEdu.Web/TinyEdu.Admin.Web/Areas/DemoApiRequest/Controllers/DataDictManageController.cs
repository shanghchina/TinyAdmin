using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using TinyEdu.Util;
using TinyEdu.Util.Model;
using TinyEdu.Entity;
using TinyEdu.Model;
using TinyEdu.Admin.Web.Controllers;
using TinyEdu.Entity.SystemManage;
using TinyEdu.Business.SystemManage;
using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Enum;

namespace TinyEdu.Admin.Web.Areas.DataDictManage.Controllers
{
    [Area("DemoApiRequest")]
    public class DataDictManageController : Controller
    {
        /// <summary>
        /// 查询界面
        /// </summary>
        /// <returns></returns>
        // GET: DataDictManage
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 编辑界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }
    }
}