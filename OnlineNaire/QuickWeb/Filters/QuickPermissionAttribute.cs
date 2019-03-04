using Quick.Common;
using Quick.Common.Net;
using Quick.Models.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickWeb.Filters
{
    public class QuickPermissionAttribute : AuthorizeAttribute
    {
        protected string ControllerName = string.Empty;

        protected string ActionName = string.Empty;

        protected bool IsAjax = false;

        protected bool IsDebug = ConfigurationManager.AppSettings["IsDebug"] != null && bool.Parse(ConfigurationManager.AppSettings["IsDebug"]);

        #region Session相关

        protected bool IsUserLogin()
        {
            return HttpContext.Current.Session.Get<UserInfoOutputDto>(QuickKeys.USER_SESSION) != null;
        }

        protected UserInfoOutputDto GetUserSession()
        {
            return HttpContext.Current.Session.Get<UserInfoOutputDto>(QuickKeys.USER_SESSION);
        }

        #endregion

        /// <summary>
        /// 授权对象
        /// </summary>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            IsAjax = filterContext.HttpContext.Request.IsAjaxRequest();
            ControllerName = filterContext.RouteData.Values["controller"]?.ToString();
            ActionName = filterContext.RouteData.Values["action"]?.ToString();

            // 调试模式
            if (IsDebug)
                return;

            // 放过 不需要登陆的页面 (找回密码页面等等...)
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length > 0)
            {
                filterContext.HttpContext.SkipAuthorization = true;
                return;
            }

            // 拦截 未登录用户
            if (!IsUserLogin())
            {
                filterContext.Result = new ContentResult() { Content = QuickKeys.USER_LOGIN };
                return;
            }

            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 授权逻辑
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }

        /// <summary>
        /// 无权操作
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}