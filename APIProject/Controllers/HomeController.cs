using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.DataAccess;
using Newtonsoft.Json;

namespace APIProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        [HttpGet]
        public ActionResult Account(string id) 
        {
            //ViewBag.Message = "Your Account page.";
            //return View();

            return Json(SQLDataAccess.LoadAccDictTable(id), JsonRequestBehavior.AllowGet);
        }
    }
}