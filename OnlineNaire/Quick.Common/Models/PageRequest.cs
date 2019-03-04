/* ==============================================================================
* 命名空间：Quick.Common.Models
* 类 名 称：PageRequest
* 创 建 者：Qing
* 创建时间：2018-11-30 10:38:44
* CLR 版本：4.0.30319.42000
* 保存的文件名：PageRequest
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

namespace Quick.Common.Models
{
    [Serializable]
    public class PageRequest
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; } = 12;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount = 0;
    }
}
