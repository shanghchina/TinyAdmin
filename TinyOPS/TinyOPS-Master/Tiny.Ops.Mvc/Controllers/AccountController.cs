using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.Ops.Mvc.Models;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.DomainService;

namespace Tiny.Ops.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private ISYS_DictionaryDomainService DictionaryDomainService => IoC.Resolve<ISYS_DictionaryDomainService>();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var test = DictionaryDomainService.GetDictionary(new GetDictionaryRequest());

                var userName = model.UserName;
                var password = model.Password;
                var item = new object();
                if (item != null && password == "111")
                {

                    //用Claim来构造一个ClaimsIdentity，然后调用 SignInAsync 方法。
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, userName));
                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    //登录
                    await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "账号密码错误，请重新输入");
                    return View(model);
                }

                //return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "有异常信息");
                _Log4Net.Error("Login", ex);
                return View(model);
                //return RedirectToAction("Login", "Account");
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            //退出
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login", "Account");

        }
    }
}