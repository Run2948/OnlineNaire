
/* ==============================================================================
* 命名空间：Quick.Models.Entity.Table 
* 类 名 称：Question
* 创 建 者：Qing
* 创建时间：2019-03-04 12:00:10
* CLR 版本：4.0.30319.42000
* 保存的文件名：Question
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Models.Entity.Enum;

namespace Quick.Models.Entity.Table
{
    [Table("Question")]
    public class Question : BaseEntity
    {
        /// <summary>
        /// 题目
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 题型
        /// </summary>
        public QuestionType QuestionType { get; set; } = QuestionType.Single;
        /// <summary>
        /// 答题时间(单位：s)
        /// </summary>
        public int TimeOut { get; set; } = 0;
        /// <summary>
        /// 选项A
        /// </summary>
        public string OptionA { get; set; }
        /// <summary>
        /// 选项B
        /// </summary>
        public string OptionB { get; set; }
        /// <summary>
        /// 选项C
        /// </summary>
        public string OptionC { get; set; }
        /// <summary>
        /// 选项D
        /// </summary>
        public string OptionD { get; set; }
        /// <summary>
        /// 选项E
        /// </summary>
        public string OptionE { get; set; }
        /// <summary>
        /// 选项F
        /// </summary>
        public string OptionF { get; set; }
        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool IsEditable { get; set; } = false;
        /// <summary>
        /// 自主选项
        /// </summary>
        public string OptionExtra { get; set; }

    }
}
