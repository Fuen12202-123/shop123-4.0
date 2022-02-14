using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class SpuModel
    {
        public string spuName { get; set; }
        public int memberId { get; set; }
        public string spuInfo { get; set; }
        public int spuPrice { get; set; }
        public int catalogAId { get; set; }
        public int catalogBId { get; set; }
        public string spuImg1 { get; set; }
        public string spuImg2 { get; set; }
        public string spuImg3 { get; set; }
        public string spuImg4 { get; set; }
        public string spuImg5 { get; set; }
        public string spuShow { get; set; }
        public string spuCreatedTime { get; set; }
        public string spuEditTime { get; set; }
    }
}