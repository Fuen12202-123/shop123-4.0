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


        public ActionResult upload()  //圖片上傳至Images資料夾，但與資料庫的名稱不同，待處理
        {         
            bool isSavedSuccessfully = true;
            string fname = "";         
            try
            {
                foreach (string filename in Request.Files)
                {                  
                    HttpPostedFileBase file = Request.Files[filename];
                    fname = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {                       
                        var path = Path.Combine(Server.MapPath("~/Images"));
                        string pathstring = Path.Combine(path.ToString());
                        string filename1 = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        bool isexist = Directory.Exists(pathstring);
                        if (!isexist)
                        {
                            Directory.CreateDirectory(pathstring);
                        }
                        string uploadpath = string.Format("{0}\\{1}", pathstring, filename1);
                       
                        file.SaveAs(uploadpath);                    
                    }
                }
            }
            catch (Exception)
            {
                isSavedSuccessfully = false;

            }
            if (isSavedSuccessfully)
            {

                return new JsonResult()
                {
                    Data = new
                    {
                        Message = fname
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };              
            }
            else
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        Message = "Error"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }

        public ActionResult SellerAddItem() //賣家商品資訊上傳
        {
            return View();
        }
        [HttpPost]
        public ActionResult SellerAddItem(SpuModel sm)
        {
            shop123Entities sh = new shop123Entities();
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

    }


}