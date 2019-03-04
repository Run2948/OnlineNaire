/* ==============================================================================
* 命名空间：Quick.Models.Entity
* 类 名 称：BaseEntity
* 创 建 者：Qing
* 创建时间：2018-11-30 09:24:02
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseEntity
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models.Entity
{
    /// <summary>
    /// 基类型
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long Id { get; set; }
    }
}
