using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyEdu.Admin.Web.Controllers;

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