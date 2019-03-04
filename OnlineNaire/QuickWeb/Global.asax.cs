using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Masuit.Tools.Logging;
using System.Web.Optimization;
using Quick.Common.NoSQL;
using Z.EntityFramework.Extensions;
using Masuit.Tools;
using System.Configuration;
using Quick.Common;

namespace QuickWeb
{
    public class Global : HttpApplication
    {
        /// <summary>
        /// 是否启用Redis：如果启用了，请注意保证正确配置Redis连接
        /// </summary>
        protected bool EnableRedis = ConfigurationManager.AppSettings["EnableRedis"] != null && bool.Parse(ConfigurationManager.AppSettings["EnableRedis"]);

        protected void Application_Start(object sender, EventArgs e)
        {
            // Z.EntityFramework 证书
            LicenseManager.AddLicense("67;100-MASUIT", "809739091397182EC1ECEA8770EB4218");
            // 区域注册
            AreaRegistration.RegisterAllAreas();
            // 过滤器注册
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // WebApi注册
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // 路由注册
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // Bundles注册
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            // 其他配置
            StartupConfig.Startup();
        }

        protected void Application_BeginRequest()
        {
            if (EnableRedis)
            {
                RedisHelper RedisHelper = RedisHelper.GetInstance();
#if !DEBUG
                if (Request.UserHostAddress != null && Request.UserHostAddress.IsDenyIpAddress())
                {
                    //Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                    Response.Write($"检测到您的IP（{Request.UserHostAddress}）异常，已被本站禁止访问，如有疑问，请联系站长！");
                    Response.End();
                    return;
                }
#endif
                string httpMethod = Request.HttpMethod;
                if (httpMethod.Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase) || httpMethod.Equals("HEAD", StringComparison.InvariantCultureIgnoreCase))
                {
                    Response.End();
                    return;
                }

                bool isSpider = Request.UserAgent != null && Request.UserAgent.Contains(new[] { "DNSPod", "Baidu", "spider", "Python", "bot" });
                if (isSpider) return;
                try
                {
                    var times = RedisHelper.StringIncrement("Frequency:" + Request.UserHostAddress + ":" + Request.UserAgent);
                    RedisHelper.Expire("Frequency:" + Request.UserHostAddress + ":" + Request.UserAgent, TimeSpan.FromMinutes(1));
                    if (times > 200)
                    {
                        //Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                        Response.Write($"检测到您的IP（{Request.UserHostAddress}）访问过于频繁，已被本站暂时禁止访问，如有疑问，请联系站长！");
                        Response.End();
                        return;
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        protected void Application_EndRequest()
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpException exception = ((HttpApplication)sender).Context.Error as HttpException;
            LogManager.Error(typeof(HttpApplication), exception ?? ((HttpApplication)sender).Server.GetLastError());
#if !DEBUG           
            int? errorCode = exception?.GetHttpCode() ?? 503;
            switch (errorCode)
            {
                case 401:
                    Response.Redirect("/AccessNoRight");
                    break;
                case 404:
                    Response.Redirect("/PageNotFound");
                    break;
                case 500:
                    Response.Redirect("/ServerError");
                    break;
                case 503:
                    Response.Redirect("/ServiceUnavailable");
                    break;
                default:
                    return;
            }
#endif
        }
    }
}