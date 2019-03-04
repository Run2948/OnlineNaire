
/* ==============================================================================
* 命名空间：Quick.Models.Entity.Enum 
* 类 名 称：QuestionType
* 创 建 者：Qing
* 创建时间：2019-03-04 12:03:49
* CLR 版本：4.0.30319.42000
* 保存的文件名：QuestionType
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models.Entity.Enum
{
    public enum QuestionType
    {
        /// <summary>
        /// 判断题
        /// </summary>
        [Description("判断题")]
        Binary,
        /// <summary>
        /// 单选题
        /// </summary>
        [Description("单选题")]
        Single,
        /// <summary>
        /// 多选题
        /// </summary>
        [Description("多选题")]
        Multiple,
        /// <summary>
        /// 填空题
        /// </summary>
        [Description("填空题")]
        Fill,
    }
}
