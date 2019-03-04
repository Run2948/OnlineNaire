using Quick.Common;
using QuickWeb.Controllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Masuit.Tools.Logging;

namespace QuickWeb.Controllers
{
    public class PublicController : BusinessController
    {
        #region 平台通用图形验证码
        public ActionResult VerifyCode(int l = 4, int f = 14, int h = 26)
        {
            string code = DrawingSecurityCode.GenerateCheckCode(l);
            TempData["valid_code"] = code;  // 将验证码存储到 TempData 中
            System.Web.HttpContext.Current.CreateCheckCodeImage(code, fontsize: f, height: h);
            Response.ContentType = "image/jpeg";
            return File(Encoding.UTF8.GetBytes(code), Response.ContentType);
        }
        #endregion

        #region 通用图片上传
        [HttpPost]
        public ActionResult UploadImage(string fileInput, string folderName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileInput))
                    fileInput = "file";
                if (string.IsNullOrEmpty(folderName))
                    folderName = "images";
                HttpPostedFileBase file = Request.Files[fileInput];
                if (file == null)
                    return No("上传图片为空！请勿非法提交");
                string fileName = file.FileName;
                if (string.IsNullOrEmpty(file.ContentType) || (file.ContentType).IndexOf("image/", StringComparison.Ordinal) == -1)
                {
                    return No("未识别的图片格式！请勿非法提交");
                }
                var result = FileUploadPro(file, folderName);
                return !result.Item1 ? No(result.Item2) : Ok(new { imageUrl = result.Item2 });
            }
            catch (Exception e)
            {
                LogManager.Error(nameof(PublicController), e);
                return No(e.Message);
            }
        }
        #endregion


    }
}