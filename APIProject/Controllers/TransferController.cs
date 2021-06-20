using DataLibrary.DataAccess;
using DataLibrary.Models;
using DataLibrary.Models.DbModels;
using DataLibrary.Models.JsonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIProject.Controllers
{
    public class TransferController : Controller
    {
        // GET: Transfer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetTransferRequest()
        {
            Transfer SGame = JsonLoader.JsonDeserializer<Transfer>(Request);
            TransferResponse TResult = InsertIntoTranfserTable.TransResponse(SGame);
            
            return Content(JsonConvert.SerializeObject(TResult), "application/json");
        }
    }
}