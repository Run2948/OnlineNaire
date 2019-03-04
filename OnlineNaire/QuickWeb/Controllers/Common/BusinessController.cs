using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickWeb.Controllers.Common
{
    /// <summary>
    /// 业务控制器
    /// </summary>
    public class BusinessController : BaseController
    {
        #region 通用用户验证码校验方法
        protected bool IsValidateCode(string code)
        {
            var vCode = TempData["valid_code"]?.ToString();
            if (vCode == null)
                return false;
            return code.ToLower().Equals(vCode.ToLower());
        }
        #endregion
    }
}