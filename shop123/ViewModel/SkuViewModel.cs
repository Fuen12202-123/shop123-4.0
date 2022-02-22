using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.ViewModel
{
    public class SkuViewModel
    {
        public int skuId { get; set; }
        public string skuImg { get; set; }
        public string spuName { get; set; }
        public string skuColor { get; set; }
        public string skuSize { get; set; }
        public int? skuStock { get; set; }
        public int spuId { get; set; }
    }
}