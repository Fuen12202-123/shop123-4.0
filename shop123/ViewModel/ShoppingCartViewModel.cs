using shop123.Models;
using shop123.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.ViewModel
{
    public class ShoppingCartViewModel
    {
        public string sellerId { get; set; }

       
        public IEnumerable<ShoppingcartsViewModel> Detail { get; set; }


    }


    public class ShoppingcartsViewModel
    {
        public int id { get; set; }

        public string memberId { get; set; }

        public string orderguid { get; set; }

        public Nullable<int> skuId { get; set; }

        public string spuImg1 { get; set; }

        public string orderDetailspuname { get; set; }

        public string orderDetailsize { get; set; }

        public string orderDetailcolor { get; set; }

        public Nullable<int> orderDetailprice { get; set; }

        public Nullable<int> orderDetailnum { get; set; }

        public Nullable<int> orderDetailtotalprice { get; set; }

        public string orderDetailIsApproved { get; set; }

        public Nullable<bool> @checked { get; set; }

        public Nullable<int> spuId { get; set; }

        public string sellerId { get; set; }
    }
}