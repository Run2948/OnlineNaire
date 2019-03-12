using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickWeb.Controllers
{
    public class MController : Controller
    {
        // GET: M
        [Route("m/{id}")]
        public ActionResult Index(string id)
        {
            return RedirectToAction("Index","P",new { id = id });
        }
    }
}