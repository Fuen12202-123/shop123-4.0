using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop123.Models
{ 
    [MetadataType(typeof(spuMetadata))]
public partial class spu
    {
        public class spuMetadata
        {
            [Key]
            public int id { get; set; }
           
            [DisplayName("商品名稱")]
            public string spuName { get; set; }
       
            [DisplayName("會員編號")]
            public int memberId { get; set; }

            [DisplayName("商品描述")]
            public string spuInfo { get; set; }

            [DisplayName("商品單價")]
            public int spuPrice { get; set; }
      
            [DisplayName("分類大項")]
            public int catalogAId { get; set; }
            [DisplayName("分類細項")]
            public int catalogBId { get; set; }
            [DisplayName("圖片1")]
            public string spuImg1 { get; set; }
            [DisplayName("圖片2")]
            public string spuImg2 { get; set; }
            [DisplayName("圖片3")]
            public string spuImg3 { get; set; }
            [DisplayName("圖片4")]
            public string spuImg4 { get; set; }
            [DisplayName("圖片5")]
            public string spuImg5 { get; set; }
            [DisplayName("是否上架")]
            public string spuShow { get; set; }
            [DisplayName("創建時間")]
            public System.DateTime spuCreatedTime { get; set; }
            [DisplayName("最後編輯時間")]
            public System.DateTime spuEditTime { get; set; }
        }
    }
}