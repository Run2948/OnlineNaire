using QuickWeb.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickWeb.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult MyInfoModifyView()
        {
            return PartialView();
        }

        public ActionResult PasswordModifyView()
        {
            return PartialView();
        }

        public ActionResult CompanyInfoModifyView()
        {
            return PartialView();
        }

        public ActionResult MemberManager()
        {
            return PartialView();
        }

        public ActionResult MemberPagedList(MemberRequest request)
        {
            return PartialView();
        }


        public ActionResult MemberLoginLogManager()
        {
            return View();
        }

        public ActionResult MemberLoginLogPagedList(MemberLoginLogRequest request)
        {
            return PartialView();
        }
    }
}