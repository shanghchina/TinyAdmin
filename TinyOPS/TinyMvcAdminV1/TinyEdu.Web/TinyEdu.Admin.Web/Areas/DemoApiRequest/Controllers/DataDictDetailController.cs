using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TinyEdu.Admin.Web.Areas.DemoApiRequest.Controllers
{
    [Area("DemoApiRequest")]
    public class DataDictDetailController : Controller
    {
        /// <summary>
        /// 字典明细列表
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailIndex()
        {
            return View();
        }

        /// <summary>
        /// 字典明细详情
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailEdit()
        {
            return View();
        }
    }
}