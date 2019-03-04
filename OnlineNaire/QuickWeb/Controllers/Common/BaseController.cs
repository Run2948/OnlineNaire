using Masuit.Tools.Media;
using Quick.Common.Models;
using Quick.Common.Net.Mime;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QuickWeb.Controllers.Common
{
    /// <summary>
    /// 基类控制器
    /// </summary>
    public class BaseController : Controller
    {
        #region 获取应用程序的Debug模式

        protected bool IsDebug = ConfigurationManager.AppSettings["IsDebug"] != null && bool.Parse(ConfigurationManager.AppSettings["IsDebug"]);

        #endregion

        #region StringSplit返回字符串数组

        private readonly char[] _splits = { ',', ':', '，', '：' };
        /// <summary>
        /// 通用字符串分割
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string[] Split(string str)
        {
            return str.Split(_splits, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region 通用返回JsonResult的封装

        /// <summary>
        /// 只返回响应状态和提示信息
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">消息</param>
        /// <param name="contentType">内容的类型</param>
        /// <param name="get">是否允许GET请求</param>
        /// <returns></returns>
        protected static JsonResult Build(int status, string msg, string contentType = ContentType.Json, bool get = false)
        {
            var js = new JsonResult
            {
                Data = new ResultInfo(status, msg)
            };
            if (contentType != ContentType.Json)
                js.ContentType = contentType;
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }

        /// <summary>
        /// 返回响应状态、消息和数据对象
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">信息</param>
        /// <param name="data">数据</param>
        /// <param name="contentType">内容的类型</param>
        /// <param name="get">是否允许GET请求</param>
        /// <returns></returns>
        protected static JsonResult Build(int status, string msg, object data, string contentType = ContentType.Json, bool get = false)
        {
            var js = new JsonResult
            {
                Data = new ResultInfo(status, msg, data)
            };
            if (contentType != ContentType.Json)
                js.ContentType = contentType;
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }

        /// <summary>
        /// 返回响应状态、消息、数据和跳转地址
        /// </summary>
        /// <param name="result">ResultInfo实体</param>
        /// <param name="contentType">内容的类型</param>
        /// <param name="get">是否允许GET请求</param>
        /// <returns></returns>
        protected static JsonResult Build(ResultInfo result, string contentType = ContentType.Json, bool get = false)
        {
            var js = new JsonResult
            {
                Data = new ResultInfo(result.Status, result.Msg, result.Data)
            };
            if (contentType != ContentType.Json)
                js.ContentType = contentType;
            if (get)
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return js;
        }

        /// <summary>
        /// 返回成功状态、消息和数据对象
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected static JsonResult Ok(string msg, object data)
        {
            return Build(status: 1, msg: msg, data: data, get: true);
        }

        /// <summary>
        /// 返回成功状态和数据对象
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected static JsonResult Ok(object data)
        {
            return Ok("Success", data: data);
        }

        /// <summary>
        /// 返回成功状态和消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        protected static JsonResult Ok(string msg)
        {
            return Build(status: 1, msg: msg, get: true);
        }

        /// <summary>
        /// 返回成功状态
        /// </summary>
        /// <returns></returns>
        protected static JsonResult Ok()
        {
            return Ok("Success");
        }

        /// <summary>
        /// 返回失败状态和消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        protected static JsonResult No(string msg)
        {
            return Build(status: 0, msg: msg, get: true);
        }

        /// <summary>
        /// 返回失败状态
        /// </summary>
        /// <returns></returns>
        protected static JsonResult No()
        {
            return No("Failure ");
        }

        #endregion

        #region 通用返回ResultInfo的封装

        protected ResultInfo InfoResp(int status, string msg)
        {
            return new ResultInfo(status, msg);
        }

        protected ResultInfo SuccResp(string msg)
        {
            return InfoResp(1, msg);
        }

        protected ResultInfo FailResp(string msg)
        {
            return InfoResp(0, msg);
        }
        #endregion

        #region 通用图片上传

        /// <summary>
        /// 通用图片上传
        /// </summary>
        /// <param name="fileInput">file表单参数名</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/images/teachers/...，则直接填写 teachers</param>
        /// <returns></returns>
        public (bool, string) ImageUpload(string fileInput, string folderName)
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            try
            {
                if (string.IsNullOrEmpty(fileInput))
                    fileInput = "file";
                if (string.IsNullOrEmpty(folderName))
                    folderName = "files";
                HttpPostedFileBase file = Request.Files[fileInput];
                if (file == null)
                    return (false, "上传文件为空！请勿非法提交");
                if (string.IsNullOrEmpty(file.ContentType) || (file.ContentType).IndexOf("image/", StringComparison.Ordinal) == -1)
                {
                    return (false, "未识别的文件格式！请勿非法提交");
                }
                var result = ImageUploadPro(file, folderName);
                return result;
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }

        }

        /// <summary>
        /// 通用图片上传
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/images/teachers/...，则直接填写 teachers</param>
        /// <returns></returns>
        public (bool, string) ImageUploadPro(HttpPostedFileBase file, string folderName)
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            if (file == null)
                return (false, "上传图片文件不能为空!");

            string fileName = Path.GetFileName(file.FileName); //文件名
            string fileExt = Path.GetExtension(fileName);  //文件后缀名

            if (fileExt == null)
                return (false, "无法识别的图片格式!!");

            fileExt = fileExt.ToLower();
            if (!".jpg".Equals(fileExt) && !".gif".Equals(fileExt) && !".png".Equals(fileExt) && !".ico".Equals(fileExt) && !"jpeg".Equals(fileExt))
                return (false, "上传文件格式错误!!支持后缀为.jpg、.jpeg、png、.gif、.ico格式的图片上传");

            try
            {
                var dir = $"/upload/images/{folderName}/{DateTime.Now:yyyy}/{DateTime.Now:yyyyMM}/{DateTime.Now:yyyyMMdd}/";
                DirectoryCreates(HttpContext.Request.MapPath(dir));
                string imagePath = dir + Guid.NewGuid().ToString("N") + fileExt;  //全球唯一标示符，作为文件名
                file.SaveAs(HttpContext.Request.MapPath(imagePath));
                var pic = Image.FromFile(HttpContext.Request.MapPath(imagePath));
                pic.Dispose();
                return (true, imagePath);  // 上传成功，返回全路径 
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        /// <summary>
        /// 图片上传，对图片的大小有限制
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/images/teachers/...，则直接填写 teachers</param>
        /// <param name="width">图片宽度限制</param>
        /// <param name="height">图片高度限制</param>
        /// <returns></returns>
        public (bool, string) ImageUploadPro(HttpPostedFileBase file, string folderName, int width, int height)
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            if (file == null)
            {
                return (false, "上传图片文件不能为空!");
            }
            else
            {
                try
                {
                    string fileName = Path.GetFileName(file.FileName); //文件名
                    string fileExt = Path.GetExtension(fileName);  //文件后缀名
                    if (fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".png" || fileExt == ".ico" || fileExt == "jpeg")  // 格式过滤
                    {
                        var dir = $"/upload/images/{folderName}/{DateTime.Now:yyyy}/{DateTime.Now:yyyyMM}/{DateTime.Now:yyyyMMdd}/";
                        DirectoryCreates(Request.MapPath(dir));
                        string imagePath = dir + Guid.NewGuid().ToString("N") + fileExt;  //全球唯一标示符，作为文件名
                        file.SaveAs(Request.MapPath(imagePath));
                        var pic = Image.FromFile(Request.MapPath(imagePath));
                        if (pic.Width > width || pic.Height > height)
                        {
                            pic.Dispose();
                            DeleteFile(Request.MapPath(imagePath));
                            return (false, $"图片尺寸不在(宽){width}像素 * (高){height}像素之间！不符合上传规则！");
                        }
                        pic.Dispose();
                        return (true, imagePath);  // 上传成功，返回全路径 
                    }
                    else
                    {
                        return (false, "上传文件格式错误!!支持后缀为.jpg、.jpeg、png、.gif、.ico格式的图片上传");
                    }
                }
                catch (Exception e)
                {
                    return (false, e.Message);
                }
            }
        }

        /// <summary>
        /// 图片上传，对图片的大小有限制
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/images/teachers/...，则直接填写 teachers</param>
        /// <param name="minWidth">图片最小宽度</param>
        /// <param name="minHeight">图片最小高度</param>
        /// <param name="maxWidth">图片最大宽度</param>
        /// <param name="maxHeight">图片最大高度</param>
        /// <returns></returns>
        public (bool, string) ImageUploadPro(HttpPostedFileBase file, string folderName, int minWidth, int minHeight, int maxWidth, int maxHeight)
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            if (file == null)
            {
                return (false, "上传图片文件不能为空!");
            }
            else
            {
                try
                {
                    string fileName = Path.GetFileName(file.FileName); //文件名
                    string fileExt = Path.GetExtension(fileName);  //文件后缀名
                    if (fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".png" || fileExt == ".ico" || fileExt == "jpeg")  // 格式过滤
                    {
                        var dir = $"/upload/images/{folderName}/{DateTime.Now:yyyy}/{DateTime.Now:yyyyMM}/{DateTime.Now:yyyyMMdd}/";
                        DirectoryCreates(Request.MapPath(dir));
                        string imagePath = dir + Guid.NewGuid().ToString("N") + fileExt;  //全球唯一标示符，作为文件名
                        file.SaveAs(Request.MapPath(imagePath));
                        var pic = Image.FromFile(Request.MapPath(imagePath));
                        if (pic.Width > maxWidth || pic.Width < minWidth)
                        {
                            pic.Dispose();
                            DeleteFile(Request.MapPath(imagePath));
                            return (false, $"图片宽度必须在{minWidth}像素 * {maxWidth}像素之间！不符合上传规则！");
                        }
                        if (pic.Height > maxHeight || pic.Height < minHeight)
                        {
                            pic.Dispose();
                            DeleteFile(Request.MapPath(imagePath));
                            return (false, $"图片高度必须在{minHeight}像素 * {maxHeight}像素之间！不符合上传规则！");
                        }
                        pic.Dispose();
                        return (true, imagePath);  // 上传成功，返回全路径 
                    }
                    else
                    {
                        return (false, "上传文件格式错误!!支持后缀为.jpg、.jpeg、png、.gif、.ico格式的图片上传");
                    }
                }
                catch (Exception e)
                {
                    return (false, e.Message);
                }
            }
        }

        /// <summary>
        /// 图片上传并裁剪(x y w h为裁剪的矩形区域)
        /// </summary>
        /// <param name="url">已经上传的图片的相对路径</param>
        /// <param name="folderName">如：~/upload/images/teachers/...，则直接填写 teachers</param>
        /// <param name="x">裁剪矩形左上角的 x 坐标</param>
        /// <param name="y">裁剪矩形左上角的 y 坐标</param>
        /// <param name="w">裁剪矩形的宽度</param>
        /// <param name="h">裁剪矩形的高度</param>
        /// <returns></returns>
        public (bool, string) SaveImagePicWithCutPro(string url, string folderName, int x, int y, int w, int h)
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            if (System.IO.File.Exists(Server.MapPath(url)))
            {
                try
                {
                    var dir = $"/upload/images/{folderName}/{DateTime.Now:yyyy}/{DateTime.Now:yyyyMM}/{DateTime.Now:yyyyMMdd}/";
                    DirectoryCreates(dir);
                    string filePath = dir + Guid.NewGuid().ToString("N") + Path.GetExtension(url);
                    Bitmap oldImageBitmap = new Bitmap(Server.MapPath(url));
                    Bitmap tempImageBitmap = oldImageBitmap.CutImage(new Rectangle(x, y, w, h));
                    if (tempImageBitmap != null)
                    {
                        tempImageBitmap.Save(Server.MapPath(filePath));
                        //释放资源
                        tempImageBitmap.Dispose();
                        oldImageBitmap.Dispose();
                        //删除临时文件
                        System.IO.File.Delete(Server.MapPath(url));
                        return (true, filePath);
                    }
                }
                catch (Exception e)
                {
                    return (false, e.Message);
                }
            }
            return (false, "图片不存在！");
        }

        /// <summary>
        /// 通用canvas上传图片
        /// </summary>
        /// <param name="stream">待上传的文件</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/images/teachers/...，则直接填写 teachers</param>
        /// <param name="fileExt">默认的文件名后缀</param>
        /// <returns></returns>
        public (bool, string) CanvasUploadPro(Stream stream, string folderName, string fileExt = ".png")
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            if (stream == null)
                return (false, "上传图片文件不能为空!");

            if (fileExt == null)
                return (false, "无法识别的图片格式!!");

            fileExt = fileExt.ToLower();

            if (!".jpg".Equals(fileExt) && !".gif".Equals(fileExt) && !".png".Equals(fileExt) && !".ico".Equals(fileExt) && !"jpeg".Equals(fileExt))
                return (false, "上传文件格式错误!!支持后缀为.jpg、.jpeg、png、.gif、.ico格式的图片上传");

            try
            {
                var dir = $"/upload/images/{folderName}/{DateTime.Now:yyyy}/{DateTime.Now:yyyyMM}/{DateTime.Now:yyyyMMdd}/";
                DirectoryCreates(HttpContext.Request.MapPath(dir));
                string imagePath = dir + Guid.NewGuid().ToString("N") + fileExt;  //全球唯一标示符，作为文件名
                byte[] dataOne = new byte[stream.Length];
                stream.Read(dataOne, 0, dataOne.Length);
                using (var fs = new FileStream(HttpContext.Request.MapPath(imagePath), FileMode.Create, FileAccess.Write))
                {
                    fs.Write(dataOne, 0, dataOne.Length);
                    return (true, imagePath);  // 上传成功，返回全路径 
                }
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        #endregion

        #region 通用文件上传

        /// <summary>
        /// 通用文件上传
        /// </summary>
        /// <param name="fileInput">file表单参数名</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/excels/teachers/...，则直接填写 excels/teachers</param>
        /// <returns></returns>
        public (bool, string) FileUpload(string fileInput, string folderName)
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            try
            {
                if (string.IsNullOrEmpty(fileInput))
                    fileInput = "file";
                if (string.IsNullOrEmpty(folderName))
                    folderName = "files";
                HttpPostedFileBase file = Request.Files[fileInput];
                if (file == null)
                    return (false, "上传文件为空！请勿非法提交");
                if (string.IsNullOrEmpty(file.ContentType) || (file.ContentType).IndexOf("image/", StringComparison.Ordinal) == -1)
                {
                    return (false, "未识别的文件格式！请勿非法提交");
                }
                var result = FileUploadPro(file, folderName);
                return result;
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        /// <summary>
        /// 通用文件上传
        /// </summary>
        /// <param name="file">待上传的文件</param>
        /// <param name="folderName">上传目录的名称，如：~/upload/excels/teachers/...，则直接填写 excels/teachers</param>
        /// <param name="extName">文件后缀</param>
        /// <returns></returns>
        public (bool, string) FileUploadPro(HttpPostedFileBase file, string folderName, string extName = "")
        {
            HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            HttpContext.Response.Charset = "UTF-8";
            if (file == null)
                return (false, "上传文件不能为空!");

            string fileName = Path.GetFileName(file.FileName); //文件名
            string fileExt = Path.GetExtension(fileName);  //文件后缀名

            if (fileExt == null)
                return (false, "无法识别的文件格式!!!");

            if (!string.IsNullOrEmpty(extName))
                if (!fileExt.ToLower().Equals(extName))
                    return (false, "上传文件格式错误!!");
            try
            {
                var dir = $"/upload/{folderName}/{DateTime.Now:yyyy}/{DateTime.Now:yyyyMM}/{DateTime.Now:yyyyMMdd}/";
                DirectoryCreates(HttpContext.Request.MapPath(dir));
                string filePath = dir + Guid.NewGuid().ToString("N") + fileExt;  //全球唯一标示符，作为文件名
                file.SaveAs(HttpContext.Request.MapPath(filePath));
                return (true, filePath);  // 上传成功，返回全路径  
            }
            catch (Exception)
            {
                return (false, "上传文件失败");  // 上传成功，返回全路径  
            }
        }

        #endregion

        #region 文件上传辅助方法

        /// <summary>
        /// 替换文件(绝对路径)
        /// </summary>
        /// <param name="oldFilePath"></param>
        /// <param name="folderName"></param>
        /// <param name="newFile"></param>
        /// <returns></returns>
        protected (bool, string) ReplaceFile(string oldFilePath, string folderName, HttpPostedFileBase newFile)
        {
            var result = FileUploadPro(newFile, folderName);
            if (!result.Item1) return result;
            try
            {
                if (FileExists(oldFilePath))
                    DeleteFile(oldFilePath);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除临时文件 (绝对路径)
        /// </summary>
        /// <param name="filePath">临时文件路径</param>
        protected bool DeleteFile(string filePath)
        {
            if (!FileExists(filePath))
                return false;
            FileInfo file = new FileInfo(filePath);
            try
            {
                if (file.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
                {
                    file.Attributes = FileAttributes.Normal;
                }
                System.IO.File.Delete(file.FullName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除临时目录下的所有文件(绝对路径)
        /// </summary>
        /// <param name="tempPath">临时目录路径</param>
        protected void DeleteFiles(string tempPath)
        {
            if (!DirectoryExists(tempPath))
                return;
            DirectoryInfo directory = new DirectoryInfo(tempPath);
            try
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
                    {
                        file.Attributes = FileAttributes.Normal;
                    }
                    System.IO.File.Delete(file.FullName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除临时目录下的所有文件及文件夹(绝对路径)
        /// </summary>
        /// <param name="path"></param>
        protected void DeleteFilesAndFolders(string path)
        {
            if (!System.IO.Directory.Exists(path))
                return;

            // Delete files.  
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                System.IO.File.Delete(file);
            }

            // Delete folders.  
            string[] folders = Directory.GetDirectories(path);
            foreach (var folder in folders)
            {
                DeleteFilesAndFolders(folder);
                Directory.Delete(folder);
            }
        }
        /// <summary>
        /// 文件是否占用(绝对路径)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected bool FileInUse(string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                return false;
            }
            catch
            {
                // ignore
            }
            finally
            {
                fs?.Close();
            }
            return true;
        }
        /// <summary>
        /// 文件是否存在(绝对路径)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected bool FileExists(string filePath) => System.IO.File.Exists(filePath);
        /// <summary>
        /// 文件夹是否存在(绝对路径)
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        protected bool DirectoryExists(string dirPath) => System.IO.Directory.Exists(dirPath);
        /// <summary>
        /// 文件夹创建(绝对路径)
        /// </summary>
        /// <param name="dirPath"></param>
        protected void DirectoryCreates(string dirPath)
        {
            if (!DirectoryExists(dirPath)) System.IO.Directory.CreateDirectory(dirPath);
        }

        #endregion

        #region 获取当前访客IP
        protected string GetCurrentIp()
        {
            string userIp = "127.0.0.1";
            try
            {
                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                    userIp = Request.ServerVariables["REMOTE_ADDR"];
                else
                    userIp = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(userIp))
                    userIp = Request.UserHostAddress;
                return userIp;
            }
            catch
            {
                return userIp;
            }
        }



        #endregion

        #region 获取服务器域名

        protected string GetServerDomain()
        {
            string curDomain;
            if (Request != null && Request.Url != null)
            {
                try
                {
                    string rawurl = Request.RawUrl;
                    if (!string.IsNullOrEmpty(rawurl) && !"/".Equals(rawurl))
                    {
                        curDomain = Request.Url.ToString().Replace(Request.RawUrl, "");
                    }
                    else
                    {
                        curDomain = Request.Url.ToString();
                    }
                }
                catch
                {
                    curDomain = "http://localhost:61266";
                }
            }
            else
            {
                curDomain = "http://localhost:61266";
            }
            return curDomain;
        }

        #endregion

        #region 获取服务器IP
        protected string GetServerIp()
        {
            string curIp = "";
            curIp = GetServerDomain().Replace("http://", ""); //去掉"http://"
            curIp = curIp.Substring(0, (curIp.IndexOf(':') == -1 ? curIp.Length : curIp.IndexOf(':')));//去掉端口号
            if (curIp.Split('.').Length != 3)   //如果没有三个'.'，则域名不是IP
            {
                IPAddress[] ipAddresses = Dns.GetHostAddresses(curIp);
                for (int i = ipAddresses.Length - 1; i >= 0;)
                {
                    curIp = ipAddresses[i].ToString();
                    break;
                }
            }
            if (curIp == "127.0.0.1")
            {
                IPAddress[] ipAddresse = Dns.GetHostAddresses(Dns.GetHostName());
                for (int i = ipAddresse.Length - 1; i >= 0;)
                {
                    curIp = ipAddresse[i].ToString();
                    break;
                }
            }
            return curIp;
        }
        #endregion

        #region 分页计算总页数
        protected int GetPage(PageRequest page)
        {
            int totalPage = (page.TotalCount + page.PageSize - 1) / page.PageSize;
            if (page.PageIndex > totalPage) page.PageIndex = 1;
            return totalPage;
        }

        protected int GetPage(PageRequest page, int totalCount)
        {
            int totalPage = (totalCount + page.PageSize - 1) / page.PageSize;
            if (page.PageIndex > totalPage) page.PageIndex = totalPage;
            return totalPage;
        }
        #endregion

        #region 从前台获取标签的值
        /// <summary>
        /// 从前台获取标签的值
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        protected string GetStringValueFromWeb(string tagName)
        {
            string strValue;

            if ((strValue = (Request.Form[tagName ?? ""] ?? "").Trim()) != "")
            {
                return strValue;
            }

            if ((strValue = (Request.Params[tagName ?? ""] ?? "").Trim()) != "")
            {
                return strValue;
            }

            if ((strValue = (Request.QueryString[tagName ?? ""] ?? "").Trim()) != "")
            {
                return strValue;
            }
            return "";
        }
        #endregion

        #region 通用Id非法校验
        protected bool IsIllegalId(int? id)
        {
            if (id == null || id <= 0)
                return true;
            return false;
        }
        #endregion
    }
}