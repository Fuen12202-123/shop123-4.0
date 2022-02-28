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
        shop123Entities2 db = new shop123Entities2();

        // GET: Member

        public ActionResult MemberCenter()
        {
            // 個人資料
            string memberId = User.Identity.Name;
            var member = from m in db.member.Where(m => m.memberAccount == memberId) select m;

            MemberCenter mc = new MemberCenter
            {
                id = member.First().id,
                memberName = member.First().memberName,
                memberPassword = member.First().memberPassword,
                memberEmail = member.First().memberEmail,
                memberPhone = member.First().memberPhone,
                orderSeller = db.orders.Where(s => s.sellerId == memberId).ToList().Select(s => new orderSeller()
                {
                    id = s.id,
                    receiverName = s.receiverName,
                    receiverPhone = s.receiverPhone,
                    orderCreate = (DateTime)s.orderCreateTime,
                    orderState = s.orderState,
                    memberId = s.memberId,
                    
                })
            };


            return View(mc);          

            //目前會員的訂單主檔OrderList.cshtml檢視使用orders模型
        }

        [HttpPost]
        [ActionName("memberinfo")]
        public ActionResult MemberCenter(MemberCenter x)  // 修改個人資料
        {    
            member m = db.member.FirstOrDefault(t => t.id == x.id);
            if(m != null)
            {
                m.memberName = x.memberName;
                m.memberPassword = x.memberPassword;
                m.memberEmail = x.memberEmail;
                m.memberPhone = x.memberPhone;
                
                db.SaveChanges();
            }
            return RedirectToAction("MemberCenter");
        }


        [HttpPost]
        public ActionResult MemberCenter(SpuModel sm) //使用者商品上傳
        {
            shop123Entities2 sh = new shop123Entities2();
            string userAccount = User.Identity.Name;
            var userId = (from m in sh.member where m.memberAccount == userAccount select m.memberAccount).First();

            if (sm.spuImg1 != null)
            {
                spu spu = new spu()
                {
                    spuName = sm.spuName,
                    //memberId = sm.memberId,
                    memberId = userId,   //memberId改為memberAccount
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
                sku sku = new sku(); //新增spu商品會同時連動sku增加欄位，存檔後跳轉skuList修改sku
                sku.spuId = spu.id;
                sh.sku.Add(sku);
                sh.SaveChanges();

                return RedirectToAction("test");
            }
            else
            {
                return View();
            }



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
        //新增sku
        public ActionResult Addsku()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Addsku(sku sku)
        {
            shop123Entities2 sh = new shop123Entities2();
            sh.sku.Add(sku);
            sh.SaveChanges();
            return Json(sku, JsonRequestBehavior.AllowGet);
        }
        //取得spu資料庫紀錄
        public JsonResult getSpuId(int id)
        {
            shop123Entities2 sh = new shop123Entities2();
            spu spu = sh.spu.FirstOrDefault(p => p.id == id);
            return Json(spu, JsonRequestBehavior.AllowGet);
        }
        //更新spu，圖片修改另外呼叫upload存入資料夾，這邊的img只存入sql
        public JsonResult spuEdit(spu spu)
        {
            shop123Entities2 sh = new shop123Entities2();



            spu p = sh.spu.FirstOrDefault(s => s.id == spu.id);
            if (p != null)
            {
                p.id = spu.id;
                p.memberId = spu.memberId;
                p.spuName = spu.spuName;
                p.spuPrice = spu.spuPrice;
                p.spuInfo = spu.spuInfo;
                p.spuShow = spu.spuShow;
                p.spuImg1 = spu.spuImg1;
                p.spuImg2 = spu.spuImg2;
                p.spuImg3 = spu.spuImg3;
                p.spuImg4 = spu.spuImg4;
                p.spuImg5 = spu.spuImg5;
                p.catalogAId = spu.catalogAId;
                p.catalogBId = spu.catalogBId;

                p.spuEditTime = DateTime.Now;
                sh.SaveChanges();
            }
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        //刪除spu，會連動sku
        public JsonResult itemDelete(int id)
        {
            shop123Entities2 sh = new shop123Entities2();


            var sku = (from k in sh.sku where k.spuId == id select k).ToList();

            spu spu = sh.spu.FirstOrDefault(p => p.id == id);
            if (spu != null)
            {
                sh.spu.Remove(spu);
                sh.sku.RemoveRange(sku);
                sh.SaveChanges();
            }
            return Json(spu, JsonRequestBehavior.AllowGet);
        }
        //sku清單
        public  JsonResult SkuList()
        {
            shop123Entities2 sh = new shop123Entities2();
            string usertest = User.Identity.Name;



            IEnumerable<SkuViewModel> test = from m in sh.member
                                             join s in sh.spu on m.memberAccount equals s.memberId
                                             where m.memberAccount == usertest
                                             join k in sh.sku on s.id equals k.spuId
                                             orderby s.spuEditTime descending, k.skuStock
                                             select new SkuViewModel
                                             {
                                                 spuName = s.spuName,
                                                 skuId = k.id,
                                                 skuColor = k.skuColor,
                                                 skuImg = k.skuImg,
                                                 skuSize = k.skuSize,
                                                 skuStock = k.skuStock,
                                                 spuId = (int)k.spuId
                                             };
            return Json(test, JsonRequestBehavior.AllowGet);
            //return View(test);
        }

        //sku搜尋

        public JsonResult SkuSearch(string searchText)
        {



            shop123Entities2 sh = new shop123Entities2();
            string usertest = User.Identity.Name;
            IEnumerable<SkuViewModel> test = from m in sh.member
                                             join s in sh.spu on m.memberAccount equals s.memberId
                                             where m.memberAccount == usertest
                                             join k in sh.sku on s.id equals k.spuId
                                             where s.spuName.Contains(searchText)
                                             select new SkuViewModel
                                             {
                                                 spuName = s.spuName,
                                                 skuId = k.id,
                                                 skuColor = k.skuColor,
                                                 skuImg = k.skuImg,
                                                 skuSize = k.skuSize,
                                                 skuStock = k.skuStock,
                                                 spuId = (int)k.spuId,

                                             };


            return Json(test, JsonRequestBehavior.AllowGet);

        }
        //取得sku紀錄
        public JsonResult getByid(int id)
        {
            shop123Entities2 sh = new shop123Entities2();
            sku sku = sh.sku.FirstOrDefault(m => m.id == id);
            return Json(sku, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public ActionResult SkuEdit(sku sv)
        {
            shop123Entities2 sh = new shop123Entities2();
            sku s = sh.sku.FirstOrDefault(t => t.id == sv.id);
            if (s != null)
            {
                s.id = sv.id;
                s.skuColor = sv.skuColor;
                s.skuImg = sv.skuImg;
                s.skuSize = sv.skuSize;
                s.skuStock = sv.skuStock;
                s.spuId = sv.spuId;
                sh.SaveChanges();

            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //spu、sku的CRUD介面
        public ActionResult test()
        {
            return View();
        }

    }
}