using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class EnrollModel
    {
        [Required(ErrorMessage = "必填欄位")]
        public string memberAccount { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        [MinLength(9, ErrorMessage = "最少9個字")]
        [MaxLength(12, ErrorMessage = "最多12個字")]
        public string memberPassword { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        public string memberName { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        public int memberPhone { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        public string memberEmail { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        public string memberAccess { get; set; }

        public DateTime memberCreateTime { get; set; }


    }
}