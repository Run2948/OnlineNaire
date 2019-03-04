/* ==============================================================================
* 命名空间：Quick.Models.Entity.Table
* 类 名 称：LoginRecord
* 创 建 者：Qing
* 创建时间：2018-11-30 09:34:13
* CLR 版本：4.0.30319.42000
* 保存的文件名：LoginRecord
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Models.Entity.Enum;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 用户登陆记录
    /// </summary>
    [Table("LoginRecord")]
    public class LoginRecord : BaseEntity
    {
        /// <summary>
        /// 登录点IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 登录地所在省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 地理位置
        /// </summary>
        public string PhysicAddress { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public LoginType LoginType { get; set; }

        /// <summary>
        /// 关联的用户id
        /// </summary>
        [ForeignKey("UserInfo")]
        public long UserInfoId { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
