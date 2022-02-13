using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    //存入cookie資訊
    public class MemberInformation
    {
        public int id { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string img { get; set; }
        public bool isBanned { get; set; }
        public string access { get; set; }
        public DateTime createdtime { get; set; }
    }
}