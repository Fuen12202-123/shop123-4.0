

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
        shop123Entities2 db = new shop123Entities2();
        //驗證寫法:寫在方法或動作上面
        //[Authorize(Roles = "管理員")]
        //[Authorize(Roles = "一般會員")]



        int pageSize = 20;

        public ActionResult Allspu(string searchstring, int page = 1)
        {//所有產品分頁的頁面
            List<spu> spu = null;

            spu = (new spuFactory()).queryAll();

            if (!String.IsNullOrEmpty(searchstring))
            {
                spu = (new spuFactory()).queryByKeyword(searchstring);
            }
            else
            {
                spu = (new spuFactory()).queryShowDesc();
            }
            int currentPage = page < 1 ? 1 : page;
            var result = spu.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        public ActionResult Index()
        {
            var carousel = (new carouselFactory()).queryAll();
            var spu = (new spuFactory()).queryShowDesc();

            HomeViewModel vw = new HomeViewModel();
            vw.carousels = carousel;
            vw.spu = spu;
            return View(vw);
        }


        public ActionResult _categoryA()
        {
            var catalog = (new catalogAFactory()).queryAll();
            return PartialView("_categoryA", catalog);
        }


        public ActionResult _categoryB(int catalogAId)
        {
            var catalog = (new catalogBFactory()).queryBycatA(catalogAId);
            return PartialView("_categoryB", catalog);
        }

        public ActionResult categoryPage(int catalogAId, int catalogBId, int page)
        {
            List<spu> spu = null;

            int currentPage = page < 1 ? 1 : page;

            if (catalogBId == 0)
                spu = (new spuFactory()).queryBycatA(catalogAId);
            else
                spu = (new spuFactory()).queryBycatAB(catalogAId, catalogBId);
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
            //test


            shop123Entities2 s = new shop123Entities2();
            var m = s.member.Where(p => p.memberEmail == member.memberEmail && p.memberPassword == member.memberPassword.ToString()).FirstOrDefault();



            //var memberAccount = m.memberAccount;  //m=null時擲回例外，改放進if判斷式裡面，並跳回登入頁面


            //var memberBanned=m.memberBanned;
            if (m != null)
            {
                var memberAccount = m.memberAccount;


                var memberBanned = m.memberBanned;

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
                return View();
                //return RedirectToAction("Enroll");
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
            shop123Entities2 shop = new shop123Entities2();


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