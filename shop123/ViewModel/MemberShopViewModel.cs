using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shop123.Models;

namespace shop123.ViewModel
{
    public class MemberShopViewModel
    {
        public member MB { get; set; }
        public IEnumerable<spu> MBspu { get; set; }
    }
}