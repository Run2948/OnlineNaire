/* ==============================================================================
* 命名空间：Quick.Models.Dto
* 类 名 称：UserInfoInputDto
* 创 建 者：Qing
* 创建时间：2018-11-30 09:31:49
* CLR 版本：4.0.30319.42000
* 保存的文件名：UserInfoInputDto
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masuit.Tools.Core.Validator;

namespace Quick.Models.Dto
{
    /// <summary>
    /// 用户信息输入模型
    /// </summary>
    public class UserInfoInputDto : BaseDto
    {
        public UserInfoInputDto()
        {
            IsAdmin = false;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空！"), MinLength(2, ErrorMessage = "用户名至少2个字符！"), MaxLength(24, ErrorMessage = "用户名最多允许24个字符")]
        public string Username { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Required(ErrorMessage = "昵称不能为空！"), MinLength(2, ErrorMessage = "昵称至少2个字符！"), MaxLength(48, ErrorMessage = "昵称最多允许48个字符")]
        public string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空！"), ComplexPassword]
        public string Password { get; set; }

        /// <summary>
        /// 加密盐
        /// </summary>
        [Required]
        public string SaltKey { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [IsEmail]
        public string Email { get; set; }

        /// <summary>
        /// QQ或微信
        /// </summary>
        public string QQorWechat { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }
    }
}
