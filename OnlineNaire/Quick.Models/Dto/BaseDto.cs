/* ==============================================================================
* 命名空间：Quick.Models.Dto
* 类 名 称：BaseDto
* 创 建 者：Qing
* 创建时间：2018-11-30 09:30:31
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseDto
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models.Dto
{
    /// <summary>
    /// DTO基类
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
    }
}
