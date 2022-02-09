using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using shop123.Models;

namespace shop123.ViewModel
{
    public class CDetailViewModel
    {
        public spu Spu { get; set; }


        public IEnumerable<sku> Sku { get; set; }

        public member Member { get; set; }

        public IEnumerable<CommentViewModel> Dcomment { get; set; }
    }

    public class CommentViewModel    //用來接收linq查詢出來的comment所需欄位
    {
        public int CVM_orderdetailId { get; set; }
        public string CVM_comment { get; set; }
        public string CVM_skuColor { get; set; }
        public string CVM_skuSize { get; set; }
        public string CVM_skuImg { get; set; }

    }
}