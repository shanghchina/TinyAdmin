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
        // GET: DataDictManage
        public ActionResult Index()
        {
            return View();
        }

        // POST: DataDictManage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}