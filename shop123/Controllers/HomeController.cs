              

using Newtonsoft.Json;
using PagedList;
using shop123.Models;
using shop123.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Windows.Forms;

namespace shop123.Controllers
{
    public class HomeController : Controller
    {//首頁、分類頁、商品頁、使用者註冊登入、賣家首頁
        shop123Entities db = new shop123Entities();
        //驗證寫法:寫在方法或動作上面
        //[Authorize(Roles = "管理員")]
        //[Authorize(Roles = "一般會員")]



        int pageSize = 12;

        public ActionResult Allspu(string searchstring,int page = 1)
        {//所有產品分頁的頁面
            var spu = db.spu.ToList();

            if (!String.IsNullOrEmpty(searchstring))
            {
                spu = spu.Where(s =>s.spuName.Contains(searchstring) && s.spuShow== "已上架").ToList();
            }
            else
            {
                spu = db.spu.Where(m=>m.spuShow== "已上架").OrderByDescending(m => m.spuEditTime).ToList();
            }
            int currentPage = page < 1 ? 1 : page;            
            var result = spu.ToPagedList(currentPage, pageSize);
            return View(result);
        }
        public ActionResult Index()
        {
            var carousel = db.carousel.ToList();
            var spu = db.spu.OrderByDescending(m => m.spuCreatedTime).Where(s=>s.spuShow == "已上架").ToList();

            HomeViewModel vw = new HomeViewModel();
            vw.carousels = carousel;
            vw.spu = spu;
            return View(vw);
        }


        public ActionResult _categoryA()
        {
            List<catalogA> catalog;
            catalog = db.catalogA.ToList();
            return PartialView("_categoryA", catalog);
        }


        public ActionResult _categoryB(int catalogAId)
        {
            List<catalogB> catalog;
            catalog = db.catalogB.Where(m => m.catalogAId == catalogAId).ToList();
            return PartialView("_categoryB", catalog);
        }

        public ActionResult categoryPage(int catalogAId, int catalogBId, int page)
        {
            List<spu> spu;

            int currentPage = page < 1 ? 1 : page;
            if (catalogBId == 0)
                spu = db.spu.Where(m => m.catalogAId == catalogAId && m.spuShow== "已上架").ToList();
            else
                spu = db.spu.Where(m => m.catalogAId == catalogAId && m.catalogBId == catalogBId && m.spuShow == "已上架").ToList();
            var result = spu.ToPagedList(currentPage, pageSize);
            ViewBag.catalogAId = catalogAId;
            ViewBag.catalogBId = catalogBId;
            return View(result);

        }

       
        public ActionResult Detail(int? id)
        {
            string memberId = User.Identity.Name;
            ViewBag.memberId = memberId;
            CDetailViewModel detail = null;
            if (id.HasValue)
            {
                detail = (new CDetailFactory()).queryById((int)id);
                return View(detail);
            }
            return RedirectToAction("Index");
        }
        //商品詳細頁面

        public ActionResult Checksize(int skuid, string color)
        {
            IEnumerable<string> size = from sku in db.sku
                                       where sku.spuId == skuid && sku.skuColor == color
                                       select sku.skuSize;

            return PartialView("Checksize", size);
        }//aja從資料庫找對應的size button

        public ActionResult checkSkuid(string color, string size)
        {
            var sk = from sku in db.sku
                     where sku.skuColor == color && sku.skuSize == size
                     select sku.id;
            string result = sk.First().ToString();
            return Content(result);
        }
        public ActionResult MemberShop(string mbId)
        {
            //if (mbId.)
            //{
                MemberShopViewModel MemberShop = new MemberShopViewModel();
                MemberShop.MB = db.member.FirstOrDefault(m => m.memberAccount == mbId);
                MemberShop.MBspu = db.spu.Where(s => s.memberId == mbId).ToList();
                return View(MemberShop);
            //}
            //return RedirectToAction("Index");
        }
        //賣家商場

        public ActionResult sign()
        {

            return View();
        }

        //Old version
        //public ActionResult sign(string memberEmail, int memberPassword)
        //{
        //    shop123Entities s = new shop123Entities();
        //    var m = s.member.Where(p => p.memberEmail == memberEmail && p.memberPassword == memberPassword.ToString()).FirstOrDefault();
        //    if (m == null)
        //    {
        //        //TempData["Error"] = "您輸入的帳號不存在或者密碼錯誤!";
        //        ViewBag.error = "帳號或密碼不正確";
        //        return View();
        //    }
        //    Session["Welcom"] = m.memberName + "歡迎光臨";
        //    FormsAuthentication.RedirectFromLoginPage(memberEmail, true);
        //    return RedirectToAction("List");
        //}

        [HttpPost]
        //New Version
        //Post:sign 
        public ActionResult sign(member member, string returnUrl)
        {   
            shop123Entities s = new shop123Entities();
            var m = s.member.Where(p => p.memberEmail == member.memberEmail && p.memberPassword == member.memberPassword.ToString()).FirstOrDefault();
            var memberAccount = m.memberAccount;
            var memberBanned=m.memberBanned;
            if (m != null)
            {
                if (memberBanned != true)
                {
                    MemberInformation mi = new MemberInformation();
                    string userData = JsonConvert.SerializeObject(mi);
                    //存票證
                    SetAuthenTicket(userData, memberAccount);
                    FormsAuthentication.SetAuthCookie(m.memberAccount, false);
                    Session["Welcom"] = member.memberName + "歡迎光臨";
                    if (m.memberAccess == "管理員") { return RedirectToAction("AdminIndex", "Admin"); }
                    else
                    {
                        //傳回原網頁有錯誤找不到
                        //登入傳到原位址
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        { return Redirect(returnUrl); }
                        else
                        { return RedirectToAction("Index", "Home"); }
                        //return RedirectToAction("Index");
                    }
                }
                //被禁用會員
                else
                {
                    TempData["message"] = "此帳號已被禁用，請洽管理員";
                    return View();
                }
                
            }
            else
            {
                //TempData["Error"] = "您輸入的帳號不存在或者密碼錯誤!";
                ViewBag.error = "帳號或密碼不正確";
                return RedirectToAction("Enroll");
            }
        }



        // GET: Enroll
        public ActionResult Enroll()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Enroll(EnrollModel em)
        {
            shop123Entities shop = new shop123Entities();


            member p = new member()
            {
                memberAccount = em.memberAccount,
                memberPassword = em.memberPassword,
                memberName = em.memberName,
                memberEmail = em.memberEmail,
                memberAccess = em.memberAccess.ToString(),
                memberPhone = em.memberPhone.ToString()
            };


            p.memberCreateTime = DateTime.Now;
            shop.member.Add(p);
            shop.SaveChanges();


            try
            {
                
                ViewBag.suc = "註冊成功，請重新登入";
                return RedirectToAction("sign");
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                if (e == ex.Message)
                    ModelState.AddModelError("memberEmail", "此信箱已被註冊過");
                return View();
            }
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



        //方法區塊

        //發行Cookie的驗證票
        void SetAuthenTicket(string UserData, string memberAccount)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                memberAccount,//會員帳號
                DateTime.Now,//票證發放計時開始
                DateTime.Now.AddMinutes(20),//票證有效期間
                false,//是否將 Cookie 設定成 Session Cookie
                UserData);//會員資料
                //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            // 建立Cookie
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authenticationcookie.Expires = DateTime.Now.AddHours(3);

            //將Cookie寫入回應
            Response.Cookies.Add(authenticationcookie);
        }



    }

}