using Masuit.Tools.Logging;
using Quick.IService;
using QuickWeb.Controllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 测试项目配置控制器
    /// </summary>
    public class TestController : BaseController
    {
        public ISystemSettingService SystemSettingService { get; set; }

        public ActionResult Index()
        {
            try
            {
                var list = SystemSettingService.GetAll().ToList();
                return Ok("测试完毕，请在RouteConfig中配置正式的启动页", list);
            }
            catch (Exception e)
            {
                LogManager.Error(nameof(TestController), e);
                return No(e.Message);
            }
        }
    }
}