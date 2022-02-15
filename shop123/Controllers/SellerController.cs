using shop123.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shop123.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Seller()
        {

            var seller = from p in (new Models.shop123Entities()).orders select p;
            //var sku = from k in (new Models.shop123Entities()).sku select k;
            return View(seller);
        }

        public ActionResult sellerPage()
        {
            ViewModel.sellerPage sp = new ViewModel.sellerPage();
            Models.shop123Entities shop = new Models.shop123Entities();
            var products = shop.spu.Join(shop.sku,
                x => x.id,
                y => y.spuId,
                (x, y) => new { x = sp.spu, y = sp.sku }
                );
            return View(products.ToList());
        }

        public ActionResult SellerAddItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SellerAddItem(SpuModel sm) //賣家商品上傳(不含圖片)，圖片需透過upload抓取資料
        {
            shop123Entities sh = new shop123Entities();        
            if (sm.spuImg1 != null)  //判斷是否至少有一張圖片
            {
                spu spu = new spu()
                {
                    spuName = sm.spuName,
                    memberId = sm.memberId,
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
                return RedirectToAction("List");
            }
            else
            {
                return View();
            }
        }
        public ActionResult upload()  //圖片上傳，目前會抓取圖片本身檔名，尚未附加亂數
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


    }


}