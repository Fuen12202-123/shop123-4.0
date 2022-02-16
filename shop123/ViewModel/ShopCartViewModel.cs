using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop123.ViewModel
{
    public class ShopCartViewModel
    {

        var ODSKU = db.ordersDetail.Join(db.sku,
                  k => k.skuId,
                  u => u.id,
                  (k, u) => new
                  {
                      id = k.id,
                      memberId = k.memberId,
                      orderguid = k.orderguid,
                      skuId = k.skuId,
                      spuImg1 = k.spuImg1,
                      orderDetailspunamek = k.orderDetailspuname,
                      orderDetailsize = k.orderDetailsize,
                      orderDetailcolor = k.orderDetailcolor,
                      orderDetailprice = k.orderDetailprice,
                      orderDetailnum = k.orderDetailnum,
                      orderDetailtotalprice = k.orderDetailtotalprice,
                      orderDetailIsApproved = k.orderDetailIsApproved,
                      @checked = k.@checked,
                      spuid = u.spuId,
                  }).Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否").ToList();
    }
}