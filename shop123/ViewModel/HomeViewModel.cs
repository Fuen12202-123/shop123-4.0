﻿using PagedList;
using shop123.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<carousel> carousels { get; set; }
        public IEnumerable<spu> spu { get; set; }  
        public IPagedList<shop123.Models.spu> spuList { get; set; }

        public IEnumerable<ordersDetail> ordersDetail { get; set; }
    }
}