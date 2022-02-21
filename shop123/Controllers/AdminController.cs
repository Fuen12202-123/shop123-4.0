using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shop123.Models;
using shop123.ViewModel;

namespace shop123.Controllers
{
    //要有管理員權限
    [Authorize(Roles = "管理員")]
    public class AdminController : Controller
    {
        // GET: Admin
        //管理者後台
        // GET: Admin

        //建立資料庫連線
        shop123Entities db = new shop123Entities();
        public ActionResult AdminIndex()
        {
            return View();
        }
        //呈現會員資料
        public ActionResult Member()
        {
            var member = db.member.OrderByDescending(m => m.id);
            return View(member);
        }
        //GET:會員新增
        public ActionResult MemberCreate()
        {
            return View();
        }
        //POST:會員新增+圖片功能
        [HttpPost]
        public ActionResult MemberCreate(member nMember,HttpPostedFileBase upPhotoMember)
        {
            var fileValid = true;
            //檔案小於5mb
            if (upPhotoMember.ContentLength <= 0 || upPhotoMember.ContentLength > 5242880) { fileValid = false; }
            else if (!CheckIsImages(upPhotoMember.InputStream)) { fileValid = false; }
            else if (fileValid == true)
            {
                string extension = Path.GetExtension(upPhotoMember.FileName);
                string fileName = $"{Guid.NewGuid()}{extension}";
                string savePath = Path.Combine(Server.MapPath("~/Images/members"), fileName);
                upPhotoMember.SaveAs(savePath);
                nMember.memberImg = savePath;
            }
            nMember.memberCreateTime = DateTime.Now;
            db.member.Add(nMember);
            db.SaveChanges();
            return RedirectToAction("Member");
        }
        //GET:會員修改
        public ActionResult MemberEdit(int id)
        {
            var member = db.member.Where(m => m.id == id).FirstOrDefault();
            return View(member);
        }
        //POST:會員修改
        [HttpPost]
        public ActionResult MemberEdit(member nMember, HttpPostedFileBase upPhotoMember)
        {
            int id = nMember.id;
            var member = db.member.Where(m => m.id == id).FirstOrDefault();
            var fileValid = true;
            //檔案小於5mb
            if (upPhotoMember.ContentLength <= 0 || upPhotoMember.ContentLength > 5242880) { fileValid = false; }
            else if (!CheckIsImages(upPhotoMember.InputStream)) { fileValid = false; }
            else if (fileValid == true)
            {
                string extension = Path.GetExtension(upPhotoMember.FileName);
                string fileName = $"{Guid.NewGuid()}{extension}";
                string savePath = Path.Combine(Server.MapPath("~/Images/members"), fileName);
                upPhotoMember.SaveAs(savePath);
                member.memberImg = savePath;
            }
            member.memberAccount = nMember.memberAccount;
            member.memberPassword = nMember.memberPassword;
            member.memberName = nMember.memberName;
            member.memberPhone = nMember.memberPhone;
            member.memberEmail = nMember.memberEmail;
            member.memberBanned = nMember.memberBanned;
            member.memberAccess = nMember.memberAccess;
            member.memberCreateTime = nMember.memberCreateTime;
            db.SaveChanges();
            return RedirectToAction("Member");
        }
        //會員刪除
        //TODO:刪除確認
        [HttpPost]
        public ActionResult MemberDelete(int id)
        {
            var member = db.member.Where(m => m.id == id).FirstOrDefault();
            db.member.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Member");
        }

        //呈現商品資料
        //TODO:排版要調整
        public ActionResult Spu()
        {
            var pSpu = db.spu.OrderByDescending(m => m.id);
            return View(pSpu);
        }
        //GET:商品新增
        public ActionResult SpuCreate()
        {
            return View();
        }
        //POST:商品新增
        [HttpPost]
        public ActionResult SpuCreate(spu pSpu)
        {
            pSpu.spuCreatedTime = DateTime.Now;
            pSpu.spuEditTime = DateTime.Now;
            db.spu.Add(pSpu);
            db.SaveChanges();
            return RedirectToAction("Spu");
        }
        //GET:商品修改
        public ActionResult SpuEdit(int id)
        {
            var pSpu = db.spu.Where(p => p.id == id).FirstOrDefault();
            return View(pSpu);
        }
        //POST:商品修改
        //TODO:更改確認
        [HttpPost]
        public ActionResult SpuEdit(spu pSpu)
        {
            int id = pSpu.id;
            var spu = db.spu.Where(p => p.id == id).FirstOrDefault();
            spu.spuName = pSpu.spuName;
            spu.memberId = pSpu.memberId;
            spu.spuInfo = pSpu.spuInfo;
            spu.spuPrice = pSpu.spuPrice;
            spu.catalogAId = pSpu.catalogAId;
            spu.catalogBId = pSpu.catalogBId;
            spu.spuImg1 = pSpu.spuImg1;
            spu.spuImg2 = pSpu.spuImg2;
            spu.spuImg3 = pSpu.spuImg3;
            spu.spuImg4 = pSpu.spuImg4;
            spu.spuImg5 = pSpu.spuImg5;
            spu.spuShow = pSpu.spuShow;
            spu.spuCreatedTime = pSpu.spuCreatedTime;
            spu.spuEditTime=DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Spu");
        }
        //商品刪除
        //TODO:刪除確認
       
        [HttpPost]
        public ActionResult SpuDelete(int id)
        {
            var pSpu = db.spu.Where(p => p.id == id).FirstOrDefault();
            db.spu.Remove(pSpu);
            db.SaveChanges();
            return RedirectToAction("Spu");
        }
        //GET:新增分類項目
        //TODO:分類項目資料表重新確認
        //public ActionResult OrderDetails()
        //{
        //    return View();
        //}
        ////POST:呈現會員詳細資料
        //[HttpPost]
        //public ActionResult MemberDetails(member nMember)
        //{
        //    var member = db.member.Where(m => m.id == nMember.id);
        //    db.member.Add(nMember);
        //    db.SaveChanges();
        //    return RedirectToAction("Member");
        //}
        ////GET:呈現會員詳細資料
        //public ActionResult MemberDetails()
        //{
        //    return View();
        //}
        ////POST:呈現會員詳細資料
        //[HttpPost]
        //public ActionResult MemberDetails(member nMember)
        //{
        //    var member = db.member.Where(m => m.id == nMember.id);
        //    db.member.Add(nMember);
        //    db.SaveChanges();
        //    return RedirectToAction("Member");
        //}

        //檢查是否為圖片
        [NonAction]
        public bool CheckIsImages(Stream imageStream)
        {
            bool check;
            try
            {
                System.Drawing.Image.FromStream(imageStream);
                check = true;
            }
            catch { check = false; }
            return check;
        }

    }
}