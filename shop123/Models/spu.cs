
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
    
public partial class spu
{

    public int id { get; set; }

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

    public System.DateTime spuCreatedTime { get; set; }

    public System.DateTime spuEditTime { get; set; }

}

}
