﻿using shop123.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.Models
{
    public class CDetailFactory
    {
        public CDetailViewModel queryById(int id)
        {
            shop123Entities db = new shop123Entities();
            spu Spu = db.spu.FirstOrDefault(p => p.id == id);
            var Sku = db.sku.Where(p => p.spuId == id).ToList();
            //Spu & Sku

            CDetailViewModel CDetail = new CDetailViewModel();
            CDetail.Spu = Spu;
            CDetail.Sku = Sku;

            int memberid = CDetail.Spu.memberId;
            CDetail.Member = db.member.FirstOrDefault(p => p.id == memberid);
            //Member

            IEnumerable<CommentViewModel> qComment = from c in db.comment
                                                     join s in db.sku on c.skuId equals s.id
                                                     where s.spuId == id
                                                     select new CommentViewModel
                                                     {
                                                         CVM_orderdetailId = c.orderDetailId
                                                                                  ,
                                                         CVM_comment = c.comment1
                                                                                  ,
                                                         CVM_skuColor = s.skuColor
                                                                                  ,
                                                         CVM_skuSize = s.skuSize
                                                                                  ,
                                                         CVM_skuImg = s.skuImg
                                                     };
            IEnumerable<CommentViewModel> ccoment = qComment.ToList();
            CDetail.Dcomment = ccoment;
            return CDetail;
        }
    }
}