using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Mvc;
using Quick.Common.Net.Mime;
using QuickWeb.Controllers.Common;
using QuickWeb.Models.UEditor;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 文件上传下载
    /// </summary>
    public class UploadController : BaseController
    {
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Route("download")]
        [Route("download/{path}")]
        public ActionResult Download(string path)
        {
            if (string.IsNullOrEmpty(path)) return Content("null");
            var file = Path.Combine(Server.MapPath("~/"), path.Trim('.', '/', '\\'));
            if (System.IO.File.Exists(file))
            {
                return this.ResumePhysicalFile(file, Path.GetFileName(file));
            }
            return Content("null");
        }

        /// <summary>
        /// UEditor文件上传处理
        /// </summary>
        /// <returns></returns>
        [Route("fileuploader")]
        public ActionResult FileUploader()
        {
            HttpContext context = System.Web.HttpContext.Current;
            Handler action = new NotSupportedHandler(context);
            switch (Request["action"])//管理员用
            {
                case "config":
                    action = new ConfigHandler(context);
                    break;
                case "uploadimage":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = UeditorConfig.GetStringList("imageAllowFiles"),
                        PathFormat = UeditorConfig.GetString("imagePathFormat"),
                        SizeLimit = UeditorConfig.GetInt("imageMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("imageFieldName")
                    });
                    break;
                case "uploadscrawl":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = new[] { ".png" },
                        PathFormat = UeditorConfig.GetString("scrawlPathFormat"),
                        SizeLimit = UeditorConfig.GetInt("scrawlMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    });
                    break;
                case "catchimage":
                    action = new CrawlerHandler(context);
                    break;
                case "uploadvideo":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = UeditorConfig.GetStringList("videoAllowFiles"),
                        PathFormat = UeditorConfig.GetString("videoPathFormat"),
                        SizeLimit = UeditorConfig.GetInt("videoMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("videoFieldName")
                    });
                    break;
                case "uploadfile":
                    action = new UploadHandler(context, new UploadConfig()
                    {
                        AllowExtensions = UeditorConfig.GetStringList("fileAllowFiles"),
                        PathFormat = UeditorConfig.GetString("filePathFormat"),
                        SizeLimit = UeditorConfig.GetInt("fileMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("fileFieldName")
                    });
                    break;
                case "listimage":
                    action = new ListFileManager(context, UeditorConfig.GetString("imageManagerListPath"), UeditorConfig.GetStringList("imageManagerAllowFiles"));
                    break;
                case "listfile":
                    action = new ListFileManager(context, UeditorConfig.GetString("fileManagerListPath"), UeditorConfig.GetStringList("fileManagerAllowFiles"));
                    break;
            }
            #region 用户通用方法
            //switch (Request["action"])//通用
            //{
            //    case "config":
            //        action = new ConfigHandler(context);
            //        break;
            //    case "uploadimage":
            //        action = new UploadHandler(context, new UploadConfig()
            //        {
            //            AllowExtensions = UeditorConfig.GetStringList("imageAllowFiles"),
            //            PathFormat = UeditorConfig.GetString("imagePathFormat"),
            //            SizeLimit = UeditorConfig.GetInt("imageMaxSize"),
            //            UploadFieldName = UeditorConfig.GetString("imageFieldName")
            //        });
            //        break;
            //    case "uploadscrawl":
            //        action = new UploadHandler(context, new UploadConfig()
            //        {
            //            AllowExtensions = new[] { ".png" },
            //            PathFormat = UeditorConfig.GetString("scrawlPathFormat"),
            //            SizeLimit = UeditorConfig.GetInt("scrawlMaxSize"),
            //            UploadFieldName = UeditorConfig.GetString("scrawlFieldName"),
            //            Base64 = true,
            //            Base64Filename = "scrawl.png"
            //        });
            //        break;
            //    case "catchimage":
            //        action = new CrawlerHandler(context);
            //        break;
            //}
            #endregion
            #region 管理员权限使用方法
            //UserInfoOutputDto user = HttpContext.Session.GetByRedis<UserInfoOutputDto>(SessionKey.UserInfo) ?? new UserInfoOutputDto();
            //if (user.IsAdmin)
            //{
            //    switch (Request["action"])//管理员用
            //    {
            //        case "uploadvideo":
            //            action = new UploadHandler(context, new UploadConfig()
            //            {
            //                AllowExtensions = UeditorConfig.GetStringList("videoAllowFiles"),
            //                PathFormat = UeditorConfig.GetString("videoPathFormat"),
            //                SizeLimit = UeditorConfig.GetInt("videoMaxSize"),
            //                UploadFieldName = UeditorConfig.GetString("videoFieldName")
            //            });
            //            break;
            //        case "uploadfile":
            //            action = new UploadHandler(context, new UploadConfig()
            //            {
            //                AllowExtensions = UeditorConfig.GetStringList("fileAllowFiles"),
            //                PathFormat = UeditorConfig.GetString("filePathFormat"),
            //                SizeLimit = UeditorConfig.GetInt("fileMaxSize"),
            //                UploadFieldName = UeditorConfig.GetString("fileFieldName")
            //            });
            //            break;
            //        case "listimage":
            //            action = new ListFileManager(context, UeditorConfig.GetString("imageManagerListPath"), UeditorConfig.GetStringList("imageManagerAllowFiles"));
            //            break;
            //        case "listfile":
            //            action = new ListFileManager(context, UeditorConfig.GetString("fileManagerListPath"), UeditorConfig.GetStringList("fileManagerAllowFiles"));
            //            break;
            //    }
            //} 
            #endregion
            string result = action.Process();
            return Content(result, ContentType.Json, Encoding.UTF8);
        }
    }
}