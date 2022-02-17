﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shop123.ViewModel
{
    public class MemberViewModel
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("會員帳號")]
        public string memberAccount { get; set; }
        [DisplayName("會員密碼")]
        public string memberPassword { get; set; }
        [DisplayName("會員名稱")]
        public string memberName { get; set; }
        [DisplayName("會員電話")]
        public string memberPhone { get; set; }
        [DisplayName("會員信箱")]
        public string memberEmail { get; set; }
        [DisplayName("會員圖片")]
        public string memberImg { get; set; }
        [DisplayName("會員禁用")]
        public bool memberBanned { get; set; }
        [DisplayName("會員權限")]
        public string memberAccess { get; set; }
        [DisplayName("會員成立日期")]
        public Nullable<System.DateTime> memberCreateTime { get; set; }

    }
}