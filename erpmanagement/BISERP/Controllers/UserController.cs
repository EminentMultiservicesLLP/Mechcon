﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BISERP.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(Models.Login.User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (user.IsValid(user.Username, user.Password))
        //        {
        //            FormsAuthentication.SetAuthCookie(user.Username, user.RememberMe);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Login failed, either UserID or Password is incorrect!");
        //        }
        //    }
        //    return View(user);
        //}
        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
