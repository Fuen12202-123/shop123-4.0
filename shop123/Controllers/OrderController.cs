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

            return View();
        }
        public ActionResult OrderDetailsPartial(string state)
        {
            List<OrderViewModel> vm = new List<OrderViewModel>();

            string memberId = User.Identity.Name;


            var GroupBy = db.ordersDetail
               .GroupBy(m => m.orderguid)
               .Select(c => new
               {
                   orderguid = c.Key,
                   count = c.Count()
               });
            if (state == "all")
            {
                var OD = db.orders.Join(GroupBy,
                o => o.orderguid,
                d => d.orderguid,
                (o, d) => new
                {
                    orderguid = d.orderguid,
                    memberId = o.memberId,
                    CreateTime = o.orderCreateTime,
                    count = d.count,
                    state = o.orderState,
                    sellerId = o.sellerId,
                    id = o.id,
                    receiverName = o.receiverName,
                    receiverAddress = o.receiverAddress,
                    receiverPhone = o.receiverPhone,

                })
                .OrderByDescending(od => od.CreateTime).Where(cs => cs.memberId == memberId);
                foreach (var item in OD)
                {
                    vm.Add(new OrderViewModel()
                    {
                        orderguid = item.orderguid,
                        orderCreateTime = item.CreateTime,
                        memberId = item.memberId,
                        orderState = item.state,
                        sellerId = item.sellerId,
                        id = item.id,
                        receiverName = item.receiverName,
                        receiverAddress = item.receiverAddress,
                        receiverPhone = item.receiverPhone,

                        Detail = db.ordersDetail.Where(o => o.orderguid == item.orderguid).Select(o => new OrderDetailViewModel()
                        {
                            id = o.id,
                            skuId = o.skuId,
                            orderDetailcolor = o.orderDetailcolor,
                            orderDetailsize = o.orderDetailsize,
                            orderDetailnum = o.orderDetailnum,
                            orderDetailspuname = o.orderDetailspuname,
                            orderDetailprice = o.orderDetailprice,
                            spuImg1 = o.spuImg1,
                            spuId = o.spuId
                        })
                    });
                }
            }
            else
            {

                var OD = db.orders.Join(GroupBy,
                o => o.orderguid,
                d => d.orderguid,
                (o, d) => new
                {
                    orderguid = d.orderguid,
                    memberId = o.memberId,
                    CreateTime = o.orderCreateTime,
                    count = d.count,
                    state = o.orderState,
                    sellerId = o.sellerId,
                    id = o.id,
                    receiverName = o.receiverName,
                    receiverAddress = o.receiverAddress,
                    receiverPhone = o.receiverPhone,
                })
                .OrderByDescending(od => od.CreateTime).Where(cs => cs.memberId == memberId && cs.state == state);
                foreach (var item in OD)
                {
                    vm.Add(new OrderViewModel()
                    {
                        orderguid = item.orderguid,
                        orderCreateTime = item.CreateTime,
                        memberId = item.memberId,
                        orderState = item.state,
                        sellerId = item.sellerId,
                        id = item.id,
                        receiverName = item.receiverName,
                        receiverAddress = item.receiverAddress,
                        receiverPhone = item.receiverPhone,

                        Detail = db.ordersDetail.Where(o => o.orderguid == item.orderguid).Select(o => new OrderDetailViewModel()
                        {
                            id = o.id,
                            skuId = o.skuId,
                            orderDetailcolor = o.orderDetailcolor,
                            orderDetailsize = o.orderDetailsize,
                            orderDetailnum = o.orderDetailnum,
                            orderDetailspuname = o.orderDetailspuname,
                            orderDetailprice = o.orderDetailprice,
                            spuImg1 = o.spuImg1,
                            spuId = o.spuId
                        })
                    });
                }
            }
            return PartialView("OrderDetailsPartial", vm);
        }
        [HttpPost]
        public ActionResult finish(int id)
        {
            var order = db.orders.Where(o => o.id == id).FirstOrDefault();
            order.orderState = "完成";
            db.SaveChanges();

            return RedirectToAction("OrderDetailsPartial");
        }
        [HttpPost]
        public ActionResult cancel(int id)
        {
            var order = db.orders.Where(o => o.id == id).FirstOrDefault();
            order.orderState = "待取消";
            db.SaveChanges();

            return RedirectToAction("OrderDetailsPartial");
        }
        [HttpPost]
        public ActionResult pay(int id)
        {
            var order = db.orders.Where(o => o.id == id).FirstOrDefault();
            order.orderState = "待出貨";
            db.SaveChanges();

            return RedirectToAction("OrderDetailsPartial");
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