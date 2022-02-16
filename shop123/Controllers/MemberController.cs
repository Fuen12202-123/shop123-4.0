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
        
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        

    }
}