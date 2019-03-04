
/* ==============================================================================
* 命名空间：Quick.Common.Tools 
* 类 名 称：RestApi
* 创 建 者：Qing
* 创建时间：2019-03-03 17:15:17
* CLR 版本：4.0.30319.42000
* 保存的文件名：RestApi
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RestSharp;

namespace Quick.Common.Tools
{
    public class RestApi<T> where T : class, new()
    {
        public T Get(string url, object pars)
        {
            const Method type = Method.GET;
            IRestResponse<T> res = GetApiInfo(url, pars, type);
            return res.Data;
        }
        public T Post(string url, object pars)
        {
            const Method type = Method.POST;
            IRestResponse<T> res = GetApiInfo(url, pars, type);
            return res.Data;
        }
        public T Delete(string url, object pars)
        {
            const Method type = Method.DELETE;
            IRestResponse<T> res = GetApiInfo(url, pars, type);
            return res.Data;
        }
        public T Put(string url, object pars)
        {
            const Method type = Method.PUT;
            IRestResponse<T> res = GetApiInfo(url, pars, type);
            return res.Data;
        }

        private static IRestResponse<T> GetApiInfo(string url, object pars, Method type)
        {
            var request = new RestRequest(type);
            if (pars != null)
                request.AddObject(pars);
            var client = new RestClient(RequestInfo.HttpDomain + url)
            {
                CookieContainer = new System.Net.CookieContainer()
            };
            IRestResponse<T> res = client.Execute<T>(request);
            return res;
        }
    }

    /// <summary>
    /// ** 描述：Request信息
    /// </summary>
    public class RequestInfo
    {
        /// <summary>
        /// 当前 HTTP 请求的完整 URL （包含参数信息）
        /// </summary>
        public static string Url => HttpContext.Current.Request.ServerVariables["URL"] + "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"];

        /// <summary>
        /// 获取当前域名加HTTP
        /// </summary>
        public static string HttpDomain => Http + Domain;

        /// <summary>
        /// 域名
        /// </summary>
        public static string Domain
        {
            get
            {
                if (((HttpContext.Current.Request.ServerVariables["SERVER_PORT"] == null) || (HttpContext.Current.Request.ServerVariables["SERVER_PORT"] == "")) || (HttpContext.Current.Request.ServerVariables["SERVER_PORT"] == "80"))
                {
                    return HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                }
                return (HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + ":" + HttpContext.Current.Request.ServerVariables["SERVER_PORT"]);
            }
        }
        /// <summary>
        /// http or https
        /// </summary>
        public static string Http => HttpContext.Current.Request.ServerVariables["HTTPS"].ToLower() == "on" ? "https://" : "http://";
        /// <summary>
        /// 当前 HTTP 请求的页面 URL 
        /// </summary>
        public static string Page => HttpContext.Current.Request.ServerVariables["URL"];

        public static HttpContext Context => System.Web.HttpContext.Current;

        /// <summary>
        /// 物理路径
        /// </summary>
        public static string PhysicalPath
        {
            get
            {
                string str = HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
                if (!str.EndsWith(@"\"))
                {
                    str = str + @"\";
                }
                return str;
            }
        }

        public static System.Collections.Specialized.NameValueCollection RequestQueryString => HttpContext.Current.Request.QueryString;

        /// <summary>
        /// ip地址
        /// </summary>
        public static string UserAddress
        {
            get
            {
                if ((HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null) || (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == ""))
                {
                    return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
        }
        //虚拟目录
        public static string Virtual
        {
            get
            {
                if ((HttpContext.Current.Request.ApplicationPath != null) && HttpContext.Current.Request.ApplicationPath.EndsWith("/"))
                {
                    return HttpContext.Current.Request.ApplicationPath;
                }
                return (HttpContext.Current.Request.ApplicationPath + "/");
            }
        }

        /// <summary>
        /// 获取GET或POST参数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string QueryString(string name)
        {
            return HttpContext.Current.Request[name];
        }
    }
}
