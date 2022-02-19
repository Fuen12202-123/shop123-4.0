using shop123.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shop123.Controllers
{
    [Authorize(Roles = "一般會員")]
    public class ShoppingCartController : Controller
    {
        shop123Entities db = new shop123Entities();
        // GET: ShoppingCart
        public ActionResult ShoppingCar()
        {
            //取得登入會員的帳號並指定給memberId
            string memberId = User.Identity.Name;
            //找出未成為訂單明細的資料，即購物車內容
            var orderDetails = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否").ToList();

            //View使用orderDetails模型
            return View(orderDetails);
        }
        public ActionResult ShoppingCarPartial()
        {
            //取得登入會員的帳號並指定給memberId
            string memberId = User.Identity.Name;
            //找出未成為訂單明細的資料，即購物車內容
            var orderDetails = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否").ToList();

            //View使用orderDetails模型
            return PartialView("_ShopCart",orderDetails);
        }
        public ActionResult checkout()
        {
            //取得登入會員的帳號並指定給memberId
            string memberId = User.Identity.Name;
            //找出未成為訂單明細的資料，即購物車內容
            var orderDetails = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否" && m.@checked == true).ToList();

            //View使用orderDetails模型
            return View(orderDetails);
        }

        public ActionResult AjaxMiniCar()
        {
            //取得登入會員的帳號並指定給memberId
            string memberId = User.Identity.Name;
            //找出未成為訂單明細的資料，即購物車內容
            var orderDetails = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否").ToList();
            //View使用orderDetails模型
            return Json(orderDetails, JsonRequestBehavior.AllowGet);
        }



        [HttpPost] 
        public ActionResult checkout(string receiverName,  string receiverPhone, string receiverAddress, int totalprice)
        {
            //找出會員帳號並指定給memberId
            string memberId = User.Identity.Name;

            //建立唯一的識別值並指定給guid變數，用來當做訂單編號
            //Order的OrderGuid欄位會關聯到OrderDetail的OrderGuid欄位
            //形成一對多的關係，即一筆訂單資料會對應到多筆訂單明細
            string guid = Guid.NewGuid().ToString();
            //建立訂單主檔資料
            orders order = new orders();
            order.orderguid = guid;
            order.memberId = memberId;
            order.receiverName = receiverName;            
            order.receiverAddress = receiverAddress;
            order.receiverPhone = receiverPhone;
            order.orderCreateTime = DateTime.Now;
            order.orderState = "未付款";
            order.totalPrice=totalprice;
            //order.totalPrice = totalprice;
            db.orders.Add(order);
            //找出目前會員在訂單明細中是購物車狀態的產品
            var ordersDetail = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否").ToList();

            //將購物車狀態產品的IsApproved設為"是"，表示確認訂購產品
            foreach (var item in ordersDetail)
            {
                if (item.@checked == true)
                {
                    item.orderguid = guid;
                    item.orderDetailIsApproved = "是";
                }

            }
            //更新資料庫，異動tOrder和tOrderDetail
            //完成訂單主檔和訂單明細的更新
            db.SaveChanges();
            return RedirectToAction("OrderList", "Order");
        }
      

        public ActionResult AddCar(int skuid, int quantity)
        {
            //取得會員帳號並指定給memberId
            string memberId = User.Identity.Name;
            //找出會員放入訂單明細的產品，該產品的fIsApproved為"否"

            //表示該產品是購物車狀態
            var currentCar = db.ordersDetail
                .Where(m => m.skuId == skuid && m.orderDetailIsApproved == "否" && m.memberId == memberId)
                .FirstOrDefault();
            //

            //若currentCar等於null，表示會員選購的產品不是購物車狀態
            if (currentCar == null)
            {
                //找出目前選購的產品並指定給sku
                var spusku = db.sku.Join(db.spu,
                  k => k.spuId,
                  u => u.id,
                  (k, u) => new
                  {
                      spuid = k.spuId,
                      skuid = k.id,
                      spuname = u.spuName,
                      spuimg = u.spuImg1,
                      color = k.skuColor,
                      size = k.skuSize,
                      price = u.spuPrice,
                  }).Where(cs => cs.skuid == skuid).FirstOrDefault();

                //將產品放入訂單明細，因為產品的fIsApproved為"否"，表示為購物車狀態
                ordersDetail orderDetail = new ordersDetail();
                orderDetail.memberId = memberId;
                orderDetail.skuId = skuid;
                orderDetail.spuId = spusku.spuid;
                orderDetail.spuImg1 = spusku.spuimg;
                orderDetail.orderDetailspuname = spusku.spuname;
                orderDetail.orderDetailcolor = spusku.color;
                orderDetail.orderDetailsize = spusku.size;
                orderDetail.orderDetailprice = spusku.price;
                orderDetail.orderDetailnum = quantity;
                orderDetail.orderDetailIsApproved = "否";
                orderDetail.@checked = false;
                db.ordersDetail.Add(orderDetail);
            }
            else
            {
                //若產品為購物車狀態，即將該產品數量加1
                currentCar.orderDetailnum += quantity;
            }
            db.SaveChanges();

            return RedirectToAction("ShoppingCar");
        }

        public ActionResult DeleteCar(int Id)
        {
            // 依Id找出要刪除購物車狀態的產品
            var orderDetail = db.ordersDetail.Where
                (m => m.id == Id).FirstOrDefault();
            //刪除購物車狀態的產品
            db.ordersDetail.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("ShoppingCarPartial");
        }

       
        public ActionResult skuchecked(int skuid)
        {
            string memberId = User.Identity.Name;
            var ordersDetail = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否" && m.skuId == skuid).ToList();
            foreach (var item in ordersDetail)
            {
                if (item.@checked == true)
                {
                    item.@checked = false;
                }
                else
                {
                    item.@checked = true;
                }
            }
            db.SaveChanges();
            return RedirectToAction("ShoppingCarPartial");
        }

            public ActionResult minus(int skuid)
        {
            string memberId = User.Identity.Name;
            var ordersDetail = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否" && m.skuId == skuid).ToList();
            foreach (var item in ordersDetail)
            {
                item.orderDetailnum -= 1;
            }
            db.SaveChanges();
            return RedirectToAction("ShoppingCarPartial");
        }

            public ActionResult plus(int skuid)
        {
            string memberId = User.Identity.Name;
            var ordersDetail = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否" && m.skuId == skuid).ToList();
            foreach (var item in ordersDetail)
            {
                item.orderDetailnum += 1;
            }
            db.SaveChanges();
            return RedirectToAction("ShoppingCarPartial");
        }

    }
}