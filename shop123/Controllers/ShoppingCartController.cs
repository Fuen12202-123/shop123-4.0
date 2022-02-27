using shop123.Models;
using shop123.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static shop123.ViewModel.ShoppingCartViewModel;

namespace shop123.Controllers
{
    [Authorize(Roles = "一般會員")]
    public class ShoppingCartController : Controller
    {
        shop123Entities2 db = new shop123Entities2();
        // GET: ShoppingCart
        public ActionResult ShoppingCar()
        {
            return View();
        }

        public ActionResult ShoppingCarPartial()
        {
            string memberId = User.Identity.Name;
            var vm = (new ordersDetailFactory()).queryByjoinspushopcart(memberId);
            return PartialView("_ShopCart", vm);
        }


        public ActionResult AjaxMiniCar()
        {
            string memberId = User.Identity.Name;
            var orderDetails = (new ordersDetailFactory()).queryBymemberId(memberId);
            return Json(orderDetails, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _skuselect(int spuid, int odid)
        {
            var sku = (new skuFactory()).queryByspuid(spuid);
            ViewBag.odid = odid;
            return PartialView("_skuselect", sku);
        }
        [HttpPost]
        public ActionResult _skuselect(int odid, string color, string size)
        {
            (new ordersDetailFactory()).updatesku(odid, color, size);
            return RedirectToAction("ShoppingCarPartial");
        }
        public ActionResult checkout()
        {

            string memberId = User.Identity.Name;
            var vm = (new ordersDetailFactory()).queryByjoinspucheckout(memberId);

            return View(vm);
        }

        [HttpPost]
        public ActionResult checkout(string[] receiverName, string[] receiverPhone, string[] receiverAddress, string[] sellerIds, string[] ordermessage, string[] paytype)
        {
            string memberId = User.Identity.Name;
            (new ordersDetailFactory()).checkout(receiverName, receiverPhone, receiverAddress, sellerIds, ordermessage, paytype, memberId);
            return RedirectToAction("OrderDetails", "Order");
        }


        public ActionResult AddCar(int skuid, int quantity)
        {

            string memberId = User.Identity.Name;
            (new ordersDetailFactory()).AddCar(skuid, quantity, memberId);
            return RedirectToAction("ShoppingCar");
        }

        public ActionResult Buyagain(int id)
        {
            string memberId = User.Identity.Name;
            (new ordersDetailFactory()).Buyagain(id,memberId);
            return RedirectToAction("ShoppingCar");
        }

        public ActionResult DeleteCar(int Id)
        {
            (new ordersDetailFactory()).DeleteCar(Id);
            return RedirectToAction("ShoppingCarPartial");
        }

        public ActionResult skuchecked(int skuid)
        {
            string memberId = User.Identity.Name;
            (new ordersDetailFactory()).skuchecked(skuid, memberId);
            return RedirectToAction("ShoppingCarPartial");
        }

        public ActionResult minus(int skuid)
        {
            string memberId = User.Identity.Name;
            (new ordersDetailFactory()).minus(skuid, memberId);
            return RedirectToAction("ShoppingCarPartial");
        }

        public ActionResult plus(int skuid)
        {
            string memberId = User.Identity.Name;
            (new ordersDetailFactory()).plus(skuid, memberId);
            return RedirectToAction("ShoppingCarPartial");
        }

    }
}