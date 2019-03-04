using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 错误处理控制器
    /// </summary>
    public class ErrorController : Controller
    {
        [Route("AccessNoRight")]
        public ActionResult AccessNoRight()
        {
            //Response.StatusCode = 401;
            if (Request.HttpMethod.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 401,
                Success = false,
                Message = "没有权限！"
            }, JsonRequestBehavior.AllowGet);
        }

        [Route("PageNotFound")]
        public ActionResult PageNotFound()
        {
            //Response.StatusCode = 404;
            if (Request.HttpMethod.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 404,
                Success = false,
                Message = "页面未找到！"
            }, JsonRequestBehavior.AllowGet);
        }

        [Route("ServerError")]
        public ActionResult ServerError()
        {
            //Response.StatusCode = 500;
            if (Request.HttpMethod.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 500,
                Success = false,
                Message = "服务器内部错误！"
            }, JsonRequestBehavior.AllowGet);
        }

        [Route("ServiceUnavailable")]
        public ActionResult ServiceUnavailable()
        {
            //Response.StatusCode = 503;
            if (Request.HttpMethod.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 503,
                Success = false,
                Message = "服务不可用！"
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 自定义错误页面1
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Request.HttpMethod.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                status = 0,
                msg = "Error",
                data = "系统异常"
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 自定义错误页面2
        /// </summary>
        /// <returns></returns>
        public ActionResult ParamsError()
        {
            if (Request.HttpMethod.ToLower().Equals("get"))
            {
                return View("~/Views/Error/ParamsError.cshtml");
            }
            return Json(new
            {
                status = 0,
                msg = "ParamsError",
                data = "请求参数异常"
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 自定义错误页面2
        /// </summary>
        /// <returns></returns>
        public ActionResult NoOrDeleted()
        {
            if (Request.HttpMethod.ToLower().Equals("get"))
            {
                return View("~/Views/Error/NoOrDeleted.cshtml");
            }
            return Json(new
            {
                status = 0,
                msg = "NoOrDeleted",
                data = "请求的数据不存在或已经被删除"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}