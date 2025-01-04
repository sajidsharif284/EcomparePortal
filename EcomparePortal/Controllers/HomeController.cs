﻿using EcomparePortal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomparePortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");

            }

            else if (User.IsInRole("Company"))
            {
                return RedirectToAction("CompanyProduct", "Company");

            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}