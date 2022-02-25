using System;
using System.Collections.Generic;
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

            var seller = from p in (new Models.shop123Entities2()).orders select p;
            //var sku = from k in (new Models.shop123Entities()).sku select k;
            return View(seller);
        }

        public ActionResult sellerPage()
        {
            ViewModel.sellerPage sp = new ViewModel.sellerPage();
            Models.shop123Entities2 shop = new Models.shop123Entities2();
            var products = shop.spu.Join(shop.sku,
                x => x.id,
                y => y.spuId,
                (x, y) => new { x = sp.spu, y = sp.sku }
                );
            return View(products.ToList());
        }
    }


}