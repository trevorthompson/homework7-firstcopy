//Name:Trevor Thompson
//Date: 3/22/16
//Assignment: homework 5 member tracker
//Description: controller for the home page
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Thompson_Trevor_HW7.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}