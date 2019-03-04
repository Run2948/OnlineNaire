/* ==============================================================================
* 命名空间：Quick.Models.Common
* 类 名 称：DbProvider
* 创 建 者：Qing
* 创建时间：2018-12-19 20:52:19
* CLR 版本：4.0.30319.42000
* 保存的文件名：DbProvider
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Configuration.ConfigurationManager;

namespace Quick.Models.Common
{
    public class DbProvider
    {
        /// <summary>
        /// 获取DbContext链接字符串,例如：
        ///   public DefaultDbContext()
        ///    :base(QuickDbProvider.GetDataBaseProvider())
        /// </summary>
        /// <returns></returns>
        public static string GetDataBaseProvider()
        {
            return $"name={GetProviderName()}";
        }

        /// <summary>
        /// 获取链接字符串名称:
        ///     例如：MsSqlDbContext
        /// </summary>
        /// <returns></returns>
        public static string GetProviderName()
        {
            return "DataContext";
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns>string</returns>
        public static string GetDbConStr()
        {
            return ConnectionStrings[GetProviderName()].ConnectionString;
        }
    }
}
