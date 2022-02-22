using shop123.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.ViewModel
{
    public class OrderViewModel
    {
      
        public int id { get; set; }

        public string memberId { get; set; }

        public Nullable<int> deliveryId { get; set; }

        public Nullable<System.DateTime> orderCreateTime { get; set; }

        public Nullable<System.DateTime> orderPaymentTime { get; set; }

        public string receiverName { get; set; }

        public string receiverAddress { get; set; }

        public string receiverEmail { get; set; }

        public string receiverPhone { get; set; }

        public string orderState { get; set; }

        public Nullable<int> totalPrice { get; set; }

        public Nullable<int> refundId { get; set; }

        public string orderguid { get; set; }

        public string sellerId { get; set; }

        public IEnumerable<OrderDetailViewModel> Detail { get; set; }

    }

    public class OrderDetailViewModel
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
    public class orderCommentViewModel
    {
        public sku SKU { get; set; }
        public int orderdetailID { get; set; }
        public int mbID { get; set; }
        public string prodName { get; set; }
    }
    public class orderCommentPostViewModel
    {
        public string txtmbID { get; set; }
        public int txtorderDetailId { get; set; }
        public string txtComments { get; set; }

        public int txtScore { get; set; }
        public int txtSkuId { get; set; }
    }
}