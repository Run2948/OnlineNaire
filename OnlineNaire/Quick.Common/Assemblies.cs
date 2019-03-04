/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：Assemblies
* 创 建 者：Qing
* 创建时间：2018-11-29 23:24:53
* CLR 版本：4.0.30319.42000
* 保存的文件名：Assemblies
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common
{
    /// <summary>
    /// 程序集缓存
    /// </summary>
    public static class Assemblies
    {
        public static Assembly ServiceAssembly { get; set; } = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", ConfigurationManager.AppSettings["BllPath"] ?? "Quick.Service.dll"));
        public static Assembly RepositoryAssembly { get; set; } = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", ConfigurationManager.AppSettings["DalPath"] ?? "Quick.Repository.dll"));
    }
}
