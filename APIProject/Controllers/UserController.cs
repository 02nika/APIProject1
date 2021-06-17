using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.DataAccess;
using Newtonsoft.Json;
using DataLibrary.Models;
using DataLibrary.Models.UserBalance;

namespace APIProject.Controllers
{
    public class UserController : Controller
    {
        // GET: Json
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JsonRequest(int? id)
        {
            AcctID input = JsonLoader.JsonDeserializer<AcctID>(Request);
            JsonAuthorize JResult = SQLDataAccess.LoadJsonRequest(input);

            return Content(JsonConvert.SerializeObject(JResult), "application/json");
        }


        [HttpPost]
        public ActionResult GetBalanceRequest(int? id)
        {

            AcctID input = JsonLoader.JsonDeserializer<AcctID>(Request);
            JsonAuthorize JResult = SQLDataAccess.LoadJsonRequest(input);
            
            return Content(JsonConvert.SerializeObject(JResult), "application/json");
        }
    }
}