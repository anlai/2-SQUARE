﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2SQUARE.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InvalidStep(string message)
        {
            return View(message);
        }

        public ActionResult Security(string message)
        {
            return View(message);
        }

        public ActionResult NoAccessToStep()
        {
            return View();
        }

        /// <summary>
        /// Not authorized to access a project resource
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Notauthorized(int id)
        {
            ViewData["ProjectId"] = id;

            return View();
        }
    }
}
