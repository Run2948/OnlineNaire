/* ==============================================================================
* 命名空间：Quick.Models.Dto
* 类 名 称：UserInfoOutputDto
* 创 建 者：Qing
* 创建时间：2018-11-30 09:31:27
* CLR 版本：4.0.30319.42000
* 保存的文件名：UserInfoOutputDto
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models.Dto
{
    /// <summary>
    /// 用户信息输出模型
    /// </summary>
    public class UserInfoOutputDto : BaseDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }

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
