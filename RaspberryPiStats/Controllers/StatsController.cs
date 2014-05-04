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
        public JsonResult StatsPerSecond()
        {
            var theColleciton = GetCollection();
            var query = theColleciton.AsQueryable<ServerStat>().ToList();

            var someStats = query
                  .GroupBy(stat =>
                      new
                      {
                          Time = string.Format("{0:MM/dd/yyyy H:mm:ss}", stat.creationDate),
                          NodeName = stat.osHostName,
                          RunName = stat.runName
                      })
                  .Select(stat => new
                  {
                      StatPerSecond = stat.Count(),
                      Node = stat.Key.NodeName,
                      RunName = stat.Key.RunName
                  })
                  .ToList();

            var jsonObject = new JsonResult();
            jsonObject.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jsonObject.MaxJsonLength = int.MaxValue;
            jsonObject.Data = someStats;
            return jsonObject;
        }

        [HttpGet]
        public JsonResult MemoryUsageOverTime(string node, string runName)
        {
            if (string.IsNullOrEmpty(node) ||
                string.IsNullOrEmpty(runName))
            {
                return new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new { }
                    };
            }

            var theColleciton = GetCollection();
            var someStats = theColleciton.AsQueryable<ServerStat>()
            .Where(stat => stat.osHostName.ToLower() == node.ToLower() &&
                stat.runName.ToLower() == runName.ToLower())
            .Select(stat => new
            {
                MemoryUsage = stat.osTotalMemory - stat.osFreeMemory,
                CurrentTime = stat.startDate
            })
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