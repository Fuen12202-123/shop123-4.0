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
    using System.ComponentModel.DataAnnotations;

    public partial class ordersDetail
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
     [Required(ErrorMessage ="請輸入數字")] 
     
    public Nullable<int> orderDetailnum { get; set; }

    public Nullable<int> orderDetailtotalprice { get; set; }

    public string orderDetailIsApproved { get; set; }

    public Nullable<bool> @checked { get; set; }

    public Nullable<int> spuId { get; set; }

}

}
