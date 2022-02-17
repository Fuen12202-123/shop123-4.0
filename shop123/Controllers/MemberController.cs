using shop123.Models;
using shop123.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace shop123.Controllers
{
    //要有會員權限
    [Authorize(Roles = "一般會員")]
    public class MemberController : Controller
    {
        shop123Entities db = new shop123Entities();

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShoppingCar()
        {
            //取得登入會員的帳號並指定給memberId
            string memberId = User.Identity.Name;
            //找出未成為訂單明細的資料，即購物車內容
            var orderDetails = db.ordersDetail.Where(m => m.memberId == memberId && m.orderDetailIsApproved == "否").ToList();

            //View使用orderDetails模型
            return View(orderDetails);
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
        //public ActionResult ShoppingCar(string receiverName, string receiverEmail, string receiverPhone, string receiverAddress, int totalprice)
        public ActionResult ShoppingCar(string receiverName, string receiverEmail, string receiverPhone, string receiverAddress)
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
            order.receiverEmail = receiverEmail;
            order.receiverAddress = receiverAddress;
            order.receiverPhone = receiverPhone;
            order.orderCreateTime = DateTime.Now;
            order.orderState = "未付款";
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
            return RedirectToAction("OrderDetails");
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
            return RedirectToAction("ShoppingCar");
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
            return RedirectToAction("ShoppingCar");
        }

        public ActionResult EditCount(int ProductID, int ProductCount)
        {
            string memberId = User.Identity.Name;
            ordersDetail od = db.ordersDetail.AsEnumerable().FirstOrDefault(c => c.skuId == ProductID && c.memberId==memberId);
            od.orderDetailnum = ProductCount;
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }

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
        public ActionResult MemberCenter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MemberCenter(SpuModel sm) //使用者商品上傳
        {
            shop123Entities sh = new shop123Entities();
            string userAccount = User.Identity.Name;
            var userId = (from m in sh.member where m.memberAccount == userAccount select m.id).First();
            if (sm.spuImg1 != null)
            {
                spu spu = new spu()
                {
                    spuName = sm.spuName,
                    //memberId = sm.memberId,
                    memberId=userId,
                    spuInfo = sm.spuInfo,
                    spuPrice = sm.spuPrice,
                    catalogAId = sm.catalogAId,
                    catalogBId = sm.catalogBId,
                    spuImg1 = sm.spuImg1,
                    spuImg2 = sm.spuImg2,
                    spuImg3 = sm.spuImg3,
                    spuImg4 = sm.spuImg4,
                    spuImg5 = sm.spuImg5,
                    spuShow = sm.spuShow,

                };
                spu.spuCreatedTime = DateTime.Now;
                spu.spuEditTime = DateTime.Now;
                sh.spu.Add(spu);
                sh.SaveChanges();
                return RedirectToAction("SellerItemEdit");
            }
            else
            {
                return View();
            }

            //return View();


            //目前會員的訂單主檔OrderList.cshtml檢視使用orders模型

        }
        public ActionResult upload()   //照片上傳至Images資料夾
        {



            string fname = "";

            foreach (string filename in Request.Files)
            {

                HttpPostedFileBase file = Request.Files[filename];
                if (file != null && file.ContentLength > 0)
                {
                    fname = file.FileName;
                    file.SaveAs(Server.MapPath("~/Images/" + fname));
                }
                else
                {
                    return View();
                }

            }


            return new JsonResult()
            {
                Data = new
                {
                    Message = fname
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult OrderDetail(string orderguid)
        {
            //根據fOrderGuid找出和訂單主檔關聯的訂單明細，並指定給orderDetails
            var orderDetails = db.ordersDetail
                .Where(m => m.orderguid == orderguid).ToList();
            //目前訂單明細的OrderDetail.cshtml檢視使用orderDetails模型
            return View(orderDetails);
        }
    }
}