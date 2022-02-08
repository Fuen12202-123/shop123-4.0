

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

        public ActionResult Detail(int? id)
        {
            if (id.HasValue)
            {
                shop123Entities db = new shop123Entities();
                spu Spu = db.spu.FirstOrDefault(p => p.id == id);
                var Sku = db.sku.Where(p => p.spuId == id).ToList();

                CDetailViewModel CDetail = new CDetailViewModel();
                CDetail.Sku = Sku;
                CDetail.Spu = Spu;
                CDetail.Comments = db.comment.ToList();
                //CDetail.Comments.Add(cmt);



                return View(CDetail);
            }
            return RedirectToAction("Index");

        }
        public ActionResult sign()
        {

            return View();
        }
        [HttpPost]
        public ActionResult sign(string memberEmail, int memberPassword)
        {
            shop123Entities s = new shop123Entities();
            var m = s.member.Where(p => p.memberEmail == memberEmail && p.memberPassword == memberPassword.ToString()).FirstOrDefault();
            if (m == null)
            {
                //TempData["Error"] = "您輸入的帳號不存在或者密碼錯誤!";
                ViewBag.error = "帳號或密碼不正確";
                return View();
            }
            Session["Welcom"] = m.memberName + "歡迎光臨";
            FormsAuthentication.RedirectFromLoginPage(memberEmail, true);
            return RedirectToAction("List");
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
    }

}