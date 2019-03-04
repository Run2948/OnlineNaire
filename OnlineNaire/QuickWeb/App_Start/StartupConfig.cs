using Quick.Common;
using Quick.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickWeb
{
    public class StartupConfig
    {
        public static void Startup()
        {
            //移除aspx视图引擎
            ViewEngines.Engines.RemoveAt(0);
            // AutoMapper映射注册 
            AutoMapperConfig.Register();
            // 数据库预热
            using (new DataContext()) { }
            // Autofac 依赖注入
            AutofacConfig.Register();
            // Hangfire 系统任务配置
            //HangfireConfig.Register();
            System.Web.Http.GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}