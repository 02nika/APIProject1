using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.DataAccess;
using Newtonsoft.Json;
using System.Text.Json;
using DataLibrary.Models;
using System.IO;
using System.Net;

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


            var JsonOBJ = SQLDataAccess.LoadAccDictTable(id);


            return Json(JsonOBJ, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult JsonRequest(int? id) 
        {
            // თავდაპირველად ვიჭერთ json რექუესთს.
            Request.InputStream.Seek(0, SeekOrigin.Begin);
            string json = new StreamReader(Request.InputStream).ReadToEnd();
            AcctID input = JsonConvert.DeserializeObject<AcctID>(json);
            //



            //ასე უნდა დავაბრუნოთ ჯეისონი
            //return Content(JsonConvert.SerializeObject(input), "application/json");
            //Json(SQLDataAccess.LoadJsonRequest(input));

            Dictionary<string, AcctID> dict1 = SQLDataAccess.LoadJsonRequest(input);
            
            return Content(JsonConvert.SerializeObject(dict1), "application/json");
        }

        [HttpGet]
        public ActionResult JsonRequest()
        {
            return View();
        }

    }
}