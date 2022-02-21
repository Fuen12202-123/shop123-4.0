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
                   count=d.count
                })
                .OrderByDescending(od=>od.CreateTime).Where(cs=>cs.memberId == memberId);

          

            List<OrderViewModel> vm = new List<OrderViewModel>();
            foreach (var item in OD)
            {
                vm.Add(new OrderViewModel()
                {
                    orderguid = item.orderguid,
                    orderCreateTime = item.CreateTime,
                    memberId = item.memberId,
                    
                    Detail = db.ordersDetail.Where(o => o.orderguid == item.orderguid).Select(o => new OrderDetailViewModel()
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
            return View(vm);
        }
          public ActionResult OrderDetailsPartial(string state)       
        {
            //找出會員帳號並指定給MemberId
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
                   state=o.orderState
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
                    
                    Detail = db.ordersDetail.Where(o => o.orderguid == item.orderguid).Select(o => new OrderDetailViewModel()
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
            return PartialView("OrderDetailsPartial", vm);
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