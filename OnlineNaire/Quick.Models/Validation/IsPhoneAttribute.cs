/* ==============================================================================
* 命名空间：Quick.Models.Validation
* 类 名 称：IsPhoneAttribute
* 创 建 者：Qing
* 创建时间：2018-11-30 09:36:53
* CLR 版本：4.0.30319.42000
* 保存的文件名：IsPhoneAttribute
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
    /// 验证手机号码是否合法
    /// </summary>
    public class IsPhoneAttribute : ValidationAttribute
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

                ErrorMessage = "手机号码不能为空";
                return false;
            }
            string phone = value as string;
            if (phone.MatchPhoneNumber())
            {
                return true;
            }
            ErrorMessage = "手机号码格式不正确，请输入有效的大陆11位手机号码！";
            return false;
        }
    }
}
