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
        public JsonResult GatherNumbers(string runname)
        {
            var theColleciton = GetCollection();
            var query = theColleciton.AsQueryable<ServerStat>()
                .Where(s => s.runName.ToLower() == runname.ToLower())
                .ToList();

            var someStats = query
                  .GroupBy(stat =>
                          new
                          {
                              NodeName = stat.osHostName
                          })
                  .Select(stat => new
                  {
                      NumberOfTransactions = stat.Count(),
                      Node = stat.Key.NodeName
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
                MemoryUsage = Math.Ceiling(Convert.ToDouble(((stat.osTotalMemory - stat.osFreeMemory) / 1048576).ToString())),
                CurrentTime = stat.startDate.Ticks
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