using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaspberryPiStats.Models
{
    public class ServerStat
    {
        public string serverAddress { get; set; }
        public int osFreeMemory { get; set; }
        public int osTotalMemory { get; set; }
        public double osUptime { get; set; }
        public string osHostName { get; set; }
        public double processUptime { get; set; }
        public string processTitle { get; set; }
        public int processId { get; set; }
        public int heapUsed { get; set; }
        public int heapTotal { get; set; }
        public int rss { get; set; }
        public ObjectId demo_id { get; set; }
        public ObjectId _id { get; set; }
        public DateTime creationDate { get; set; }
        public int __v { get; set; }
        public string runName { get; set; }
    }
}