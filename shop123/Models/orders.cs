//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace shop123.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class orders
    {
        public int id { get; set; }
        public string memberId { get; set; }
        public Nullable<int> deliveryId { get; set; }
        public Nullable<System.DateTime> orderCreateTime { get; set; }
        public Nullable<System.DateTime> orderPaymentTime { get; set; }
        public string receiverName { get; set; }
        public string receiverAddress { get; set; }
        public string receiverPhone { get; set; }
        public string orderState { get; set; }
        public Nullable<int> totalPrice { get; set; }
        public Nullable<int> refundId { get; set; }
        public string orderguid { get; set; }
        public string sellerId { get; set; }
    }
}
