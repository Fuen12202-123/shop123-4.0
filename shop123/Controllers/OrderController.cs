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
        shop123Entities2 db = new shop123Entities2();
        // GET: Order
        public ActionResult OrderDetails()
        {

            return View();
        }

        public ActionResult OrderDetailsPartial(string state)
        {
            List<OrderViewModel> vm = new List<OrderViewModel>();

            string memberId = User.Identity.Name;            
            if (state == "all")
            {
                vm = (new ordersFactory()).orderall(state, memberId);
            }
            else
            {
                vm = (new ordersFactory()).orderstate(state, memberId);
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