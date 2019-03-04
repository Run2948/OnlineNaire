/* ==============================================================================
* 命名空间：Quick.Models.Entity.Table
* 类 名 称：UserInfo
* 创 建 者：Qing
* 创建时间：2018-11-30 09:26:02
* CLR 版本：4.0.30319.42000
* 保存的文件名：UserInfo
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masuit.Tools.Core.Validator;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("UserInfo")]
    public class UserInfo : BaseEntity
    {
        public UserInfo()
        {
            IsAdmin = false;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空！")]
        public string Username { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Required(ErrorMessage = "昵称不能为空！")]
        public string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空！")]
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

        /// <summary>
        /// AccessToken，接入第三方登陆时用
        /// </summary>
        public string AccessToken { get; set; }

        public virtual ICollection<LoginRecord> LoginRecord { get; set; }
    }
}
