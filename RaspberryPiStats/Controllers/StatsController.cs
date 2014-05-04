using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RaspberryPiStats.Models;
namespace RaspberryPiStats.Controllers
{
    public class StatsController : Controller
    {
        //
        // GET: /Stats/
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetStats()
        {
            var theColleciton = GetCollection();
            var someStats = theColleciton.AsQueryable<ServerStat>()
                .Where(s => s.creationDate < DateTime.Now && s.creationDate > DateTime.Now.AddSeconds(-1))
                .Skip(0)
                .Take(50)
                .ToList();

            var jsonObject = new JsonResult();
            jsonObject.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jsonObject.MaxJsonLength = int.MaxValue;
            jsonObject.Data = someStats;
            return jsonObject;
        }

        public MongoCollection<ServerStat> GetCollection()
        {
            MongoClient client = new MongoClient("mongodb://192.168.1.246:27017/picluster");
            var server = client.GetServer();
            var database = server.GetDatabase("picluster");
            var theCollection = database.GetCollection<ServerStat>("serverstats");
            return theCollection;
        }
    }
}