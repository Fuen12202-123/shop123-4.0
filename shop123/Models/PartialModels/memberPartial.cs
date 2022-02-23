using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    [MetadataType(typeof(memberMetadata))]
    public partial class member
    {
        public class memberMetadata
        {
            public int id { get; set; }
            [Required(ErrorMessage ="必填欄位")]
            [DisplayName("會員帳號")]
            public string memberAccount { get; set; }
            
            [Required(ErrorMessage = "必填欄位")]
            [DisplayName("會員密碼")]
            public string memberPassword { get; set; }
            [Required(ErrorMessage = "必填欄位")]
            [DisplayName("會員名稱")]
            public string memberName { get; set; }
            [DisplayName("會員電話")]
            public string memberPhone { get; set; }    
            [DisplayName("會員信箱")]
            [Required(ErrorMessage ="會員信箱為必填欄位")]
            public string memberEmail { get; set; }
            [DisplayName("會員圖片")]
            public string memberImg { get; set; }
            [DisplayName("是否禁用")]
            public Nullable<bool> memberBanned { get; set; }
            [Required(ErrorMessage = "必填欄位")]
            [DisplayName("會員權限")]
            public string memberAccess { get; set; }
            [DisplayName("會員成立日期")]
            public System.DateTime memberCreateTime { get; set; }
        }
    }
    
}