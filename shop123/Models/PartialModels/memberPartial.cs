using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    [MetadataType(typeof(ordersMetadata))]
    public partial class orders
    {
        public class ordersMetadata
        {
            [Key]
            public int id { get; set; }
            [DisplayName("會員帳號")]
            public string memberId { get; set; }
            [DisplayName("物流狀態")]
            public Nullable<int> deliveryId { get; set; }
            [DisplayName("訂單建立時間")]
            public Nullable<System.DateTime> orderCreateTime { get; set; }
            [DisplayName("訂單付款時間")]
            public Nullable<System.DateTime> orderPaymentTime { get; set; }
            [DisplayName("收件人")]
            public string receiverName { get; set; }
            [DisplayName("收件地址")]
            public string receiverAddress { get; set; }
           
            [DisplayName("收件人手機")]
            public string receiverPhone { get; set; }
            [DisplayName("訂單狀態")]
            public string orderState { get; set; }
            [DisplayName("總金額")]
            public Nullable<int> totalPrice { get; set; }
            [DisplayName("退貨編號")]
            public Nullable<int> refundId { get; set; }
            [DisplayName("訂單編號")]
            public string orderguid { get; set; }
            [DisplayName("賣家帳號")]
            public string sellerId { get; set; }
        }
    }
    
}