/* ==============================================================================
* 命名空间：Quick.Models.Validation
* 类 名 称：IsEmailAttribute
* 创 建 者：Qing
* 创建时间：2018-11-30 09:35:57
* CLR 版本：4.0.30319.42000
* 保存的文件名：IsEmailAttribute
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
    /// 邮箱格式验证
    /// </summary>
    public class IsEmailAttribute : ValidationAttribute
    {
        public IsEmailAttribute()
        {
            AllowEmpty = false;
        }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowEmpty { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                if (AllowEmpty) return true;

                ErrorMessage = "邮箱不能为空！";
                return false;
            }

            var email = value as string;
            if (email.Length < 6)
            {
                ErrorMessage = "您输入的邮箱格式不正确！";
                return false;
            }

            if (email.Length > 256)
            {
                ErrorMessage = "邮箱长度最大允许255个字符！";
                return false;
            }
            if (email.MatchEmail())
            {
                return true;
            }
            ErrorMessage = "您输入的邮箱格式不正确！";
            return false;
        }
    }
}
