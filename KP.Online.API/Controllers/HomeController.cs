﻿using System;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;

namespace KP.Online.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
