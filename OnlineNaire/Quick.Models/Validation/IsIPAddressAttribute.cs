/* ==============================================================================
* 命名空间：Quick.Models.Validation
* 类 名 称：IsIPAddressAttribute
* 创 建 者：Qing
* 创建时间：2018-11-30 09:36:28
* CLR 版本：4.0.30319.42000
* 保存的文件名：IsIPAddressAttribute
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



using Masuit.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models.Validation
{
    /// <summary>
    /// 验证IPv4地址是否合法
    /// </summary>
    public class IsIPAddressAttribute : ValidationAttribute
    {
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowEmpty { get; set; } = false;

        public override bool IsValid(object value)
        {
            if (value is null)
            {
                if (AllowEmpty) return true;

                ErrorMessage = "IP地址不能为空！";
                return false;
            }
            string email = value as string;
            if (email.MatchInetAddress())
            {
                return true;
            }
            ErrorMessage = "IP地址格式不正确，请输入有效的IPv4地址";
            return false;
        }
    }
}
