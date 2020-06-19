using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TinyEdu.Admin.Web.Areas.DemoApiRequest.Controllers
{
    [Area("DemoApiRequest")]
    public class RoleManageController : Controller
    {
        /// <summary>
        /// 角色管理列表
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 角色管理修改
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            return View();
        }
    }
}