using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickWeb.Controllers
{
    public class PController : Controller
    {
        // GET: p
        [Route("p/{id}")]
        public ActionResult Index(string id)
        {
            return new Random().Next(1,10) > 5 ? View("~/Views/P/Index.cshtml") : View("~/Views/P/Stoped.cshtml");
        }
    }
}