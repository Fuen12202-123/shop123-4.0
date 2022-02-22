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
        shop123Entities db = new shop123Entities();

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
                memberPhone = member.First().memberPhone
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
            shop123Entities sh = new shop123Entities();
            string userAccount = User.Identity.Name;
            //var userId = (from m in sh.member where m.memberAccount == userAccount select m.id).First();
            if (sm.spuImg1 != null)
            {
                spu spu = new spu()
                {
                    spuName = sm.spuName,
                    //memberId = sm.memberId,
                    memberId= userAccount,
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
                return RedirectToAction("SellerItemEdit");
            }
            else
            {
                return View();
            }

            //return View();


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

        
    }
}