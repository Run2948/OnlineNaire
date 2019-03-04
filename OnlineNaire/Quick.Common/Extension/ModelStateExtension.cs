/* ==============================================================================
* 命名空间：Quick.Common.Extension
* 类 名 称：ModelStateExtension
* 创 建 者：Qing
* 创建时间：2019-01-06 13:39:25
* CLR 版本：4.0.30319.42000
* 保存的文件名：ModelStateExtension
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
using System.Web.Mvc;

namespace Quick.Common.Extension
{
    /// <summary>
    /// ModelState 扩展类
    /// </summary>
    public static class ModelStateExtension
    {
        /// <summary>
        /// 获取模型绑定中的ErrMsg
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string GetErrMsg(this ModelStateDictionary dict)
        {
            if (dict.IsValid || !dict.Any()) return "";
            foreach (string key in dict.Keys)
            {
                ModelState tmp = dict[key];
                if (tmp.Errors.Any())
                {
                    var firstOrDefault = tmp.Errors.FirstOrDefault();
                    if (firstOrDefault != null)
                        return firstOrDefault.ErrorMessage;
                }
            }
            return "";
        }
    }
}
