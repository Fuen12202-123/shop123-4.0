

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
                spu = spu.Where(s =>s.spuName.Contains(searchstring)).ToList();
            }
            else
            {
                spu = db.spu.OrderByDescending(m => m.spuEditTime).ToList();
            }
            int currentPage = page < 1 ? 1 : page;            
            var result = spu.ToPagedList(currentPage, pageSize);
            return View(result);
        }
        public ActionResult Index()
        {
            var carousel = db.carousel.ToList();
            var spu = db.spu.OrderByDescending(m => m.spuCreatedTime).ToList();

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
                spu = db.spu.Where(m => m.catalogAId == catalogAId).ToList();
            else
                spu = db.spu.Where(m => m.catalogAId == catalogAId && m.catalogBId == catalogBId).ToList();
            var result = spu.ToPagedList(currentPage, pageSize);
            ViewBag.catalogAId = catalogAId;
            ViewBag.catalogBId = catalogBId;
            return View(result);

        }

        [Authorize(Roles ="一般會員")]
        public ActionResult Detail(int? id)
        {
            CDetailViewModel detail = null;
            if (id.HasValue)
            {
                detail = (new CDetailFactory()).queryById((int)id);
                return View(detail);
            }
            return RedirectToAction("Index");

        }
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
        {   shop123Entities s = new shop123Entities();
            var m = s.member.Where(p => p.memberEmail == member.memberEmail && p.memberPassword == member.memberPassword.ToString()).FirstOrDefault();
            var memberAccount = m.memberAccount;
            if (m != null)
            {
                MemberInformation mi = new MemberInformation();
                string userData = JsonConvert.SerializeObject(mi);
                //存票證
                SetAuthenTicket(userData, memberAccount);
                FormsAuthentication.SetAuthCookie(m.memberAccount, false);
                Session["Welcom"] = member.memberName + "歡迎光臨";
                //傳回原網頁有錯誤找不到
                //TODO登入傳到原位址
                //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                //{return RedirectToAction(returnUrl);}
                //else
                //{return RedirectToAction("Index", "Home");}
                return RedirectToAction("Index");
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



            try
            {
                shop.SaveChanges();
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



        //方法區塊

        //發行Cookie的驗證票
        void SetAuthenTicket(string UserData, string memberAccount)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                memberAccount,//會員帳號
                DateTime.Now,//票證發放計時開始
                DateTime.Now.AddMinutes(30),//票證有效期間
                false,//是否將 Cookie 設定成 Session Cookie，如果是則會在瀏覽器關閉後移除
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