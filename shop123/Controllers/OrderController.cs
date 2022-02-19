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

            //找出會員帳號並指定給MemberId
            string memberId = User.Identity.Name;

            var qODG = from od in db.ordersDetail
                       group od by od.orderguid into OG
                       select new { orderguid = OG.Key, Count = OG.Count() };

            var qOD = from o in db.orders
                      join og in qODG on o.orderguid equals og.orderguid
                      where o.memberId == memberId
                      orderby o.orderCreateTime descending
                      select new { orderguid = og.orderguid, memberId = o.memberId, CreateTime = o.orderCreateTime, Count = og.Count };

            List<OrderViewModel> lsOd = new List<OrderViewModel>();
            foreach (var oGd in qOD)
            {
                lsOd.Add(new OrderViewModel()
                {
                    orderguid = oGd.orderguid,
                    orderCreateTime = oGd.CreateTime,
                    memberId = oGd.memberId,
                    Detail = db.ordersDetail.Where(o => o.orderguid == oGd.orderguid).Select(o => new OrderDetailViewModel()
                    {
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
            return View(lsOd);

        }

        public ActionResult OrderDetailsPartial()
        {
            //找出會員帳號並指定給MemberId
            string memberId = User.Identity.Name;

            var qODG = from od in db.ordersDetail
                       group od by od.orderguid into OG
                       select new { orderguid = OG.Key, Count = OG.Count() };

            var qOD = from o in db.orders
                      join og in qODG on o.orderguid equals og.orderguid
                      where o.memberId == memberId
                      orderby o.orderCreateTime descending
                      select new { orderguid = og.orderguid, memberId = o.memberId, CreateTime = o.orderCreateTime, Count = og.Count };

            List<OrderViewModel> lsOd = new List<OrderViewModel>();
            foreach (var oGd in qOD)
            {
                lsOd.Add(new OrderViewModel()
                {
                    orderguid = oGd.orderguid,
                    orderCreateTime = oGd.CreateTime,
                    memberId = oGd.memberId,
                    Detail = db.ordersDetail.Where(o => o.orderguid == oGd.orderguid).Select(o => new OrderDetailViewModel()
                    {
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
            return PartialView("ordersDetailPartial", lsOd);

        }
        public ActionResult OrderList()
        {
            //找出會員帳號並指定給MemberId
            string memberId = User.Identity.Name;
            //找出目前會員的所有訂單主檔記錄並依照fDate進行遞增排序
            //將查詢結果指定給orders
            var orders = db.orders.Where(m => m.memberId == memberId)
                .OrderByDescending(m => m.orderCreateTime).ToList();
            return View(orders);
        }
              
        public ActionResult OrderDetail(string orderguid)
        {
            
            //根據fOrderGuid找出和訂單主檔關聯的訂單明細，並指定給orderDetails
            var orderDetails = db.ordersDetail.Where(m => m.orderguid == orderguid ).ToList();
            //目前訂單明細的OrderDetail.cshtml檢視使用orderDetails模型
            return View(orderDetails);
        }
    }
}