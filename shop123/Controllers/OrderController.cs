using shop123.Models;
using shop123.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shop123.Controllers
{
    [Authorize(Roles = "一般會員")]
    public class OrderController : Controller
    {
        shop123Entities db = new shop123Entities();
        // GET: Order



        public ActionResult OrderDetails()
        {
            
            string memberId = User.Identity.Name;


            var GroupBy = db.ordersDetail
               .GroupBy(m => m.orderguid)
               .Select(c => new
               {
                   orderguid = c.Key,
                   count = c.Count()
               });

            var OD = db.orders.Join(GroupBy,
                o => o.orderguid,
                d => d.orderguid,
                (o, d) => new
                {
                    orderguid = d.orderguid,
                    memberId = o.memberId,
                    CreateTime = o.orderCreateTime,
                    sellerId = o.sellerId,
                    State=o.orderState,

                })
                .OrderByDescending(od => od.CreateTime).Where(cs => cs.memberId == memberId);



            List<OrderViewModel> vm = new List<OrderViewModel>();
            foreach (var item in OD)
            {
                vm.Add(new OrderViewModel()
                {
                    orderguid = item.orderguid,
                    orderCreateTime = item.CreateTime,
                    memberId = item.memberId,
                    sellerId = item.sellerId,
                    orderState=item.State,

                    Detail = db.ordersDetail.Where(o => o.orderguid == item.orderguid).Select(o => new OrderDetailViewModel()
                    {
                        skuId = o.skuId,
                        orderDetailcolor = o.orderDetailcolor,
                        orderDetailsize = o.orderDetailsize,
                        orderDetailnum = o.orderDetailnum,
                        orderDetailspuname = o.orderDetailspuname,
                        orderDetailprice = o.orderDetailprice,
                        spuImg1 = o.spuImg1,
                        spuId=o.spuId,
                    })
                });
            }
            return View(vm);
        }
        public ActionResult OrderDetailsPartial(string state)       
        {
            
            string memberId = User.Identity.Name;
        

            var GroupBy = db.ordersDetail
               .GroupBy(m => m.orderguid)
               .Select(c => new
               {
                   orderguid = c.Key,
                   count = c.Count()
               });

            var OD=db.orders.Join(GroupBy,
                o=> o.orderguid,
                d=>d.orderguid,
                (o, d) => new
                {
                   orderguid=d.orderguid,
                   memberId= o.memberId,
                   CreateTime = o.orderCreateTime,
                   count=d.count,
                   state=o.orderState,
                   sellerId=o.sellerId
                })
                .OrderByDescending(od=>od.CreateTime).Where(cs=>cs.memberId == memberId && cs.state==state);

            List<OrderViewModel> vm = new List<OrderViewModel>();
            foreach (var item in OD)
            {
                vm.Add(new OrderViewModel()
                {
                    orderguid = item.orderguid,
                    orderCreateTime = item.CreateTime,
                    memberId = item.memberId,
                    orderState=item.state,
                    sellerId=item.sellerId,


                    Detail = db.ordersDetail.Where(o => o.orderguid == item.orderguid).Select(o => new OrderDetailViewModel()
                    {
                        id = o.id,
                        skuId = o.skuId,
                        orderDetailcolor = o.orderDetailcolor,
                        orderDetailsize = o.orderDetailsize,
                        orderDetailnum = o.orderDetailnum,
                        orderDetailspuname = o.orderDetailspuname,
                        orderDetailprice = o.orderDetailprice,
                        spuImg1 = o.spuImg1
                    })
                });
            }
            return PartialView("OrderDetailsPartial", vm);
        }

        //顧客寫評論
        public ActionResult ProdComment(int skuID, string mbName, int orderdetailID)
        {

            orderCommentViewModel oc = new orderCommentViewModel();
            oc.SKU = db.sku.FirstOrDefault(t => t.id == skuID);
            spu sp = db.spu.FirstOrDefault(p => p.id == oc.SKU.spuId);
            oc.orderdetailID = orderdetailID;     
            oc.mbID = mbName;
            oc.prodName = sp.spuName;
            return View(oc);
        }
        //評論提交表單
        [HttpPost]
        public ActionResult ProdComment(orderCommentPostViewModel p)
        {
            if (p != null)
            {
                comment cmt = new comment()
                {
                    memberId = p.txtmbID,
                    orderDetailId = p.txtorderDetailId,
                    comment1 = p.txtComments,
                    score = p.txtScore,
                    skuId = p.txtSkuId
                };
                db.comment.Add(cmt);
                db.SaveChanges();
            }
            return RedirectToAction("OrderDetails");
        }
    }
}