
/* ==============================================================================
* 命名空间：Quick.Models.Application
* 类 名 称：DataContext
* 创 建 者：Qing
* 创建时间：2018-11-29 20:27:11
* CLR 版本：4.0.30319.42000
* 保存的文件名：DataContext
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

using Quick.Models.Entity.Table;
using System.Data.Entity;

namespace Quick.Models.Application
{
    public partial class DataContext : DbContext
    {
        #region DbSet
        /// <summary>
        /// LoginRecord
        /// </summary>
        public virtual DbSet<LoginRecord> LoginRecord { get; set; }

        /// <summary>
        /// SystemSetting
        /// </summary>
        public virtual DbSet<SystemSetting> SystemSetting { get; set; }

        /// <summary>
        /// UserInfo
        /// </summary>
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        #endregion
    }
}
