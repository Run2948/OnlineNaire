/* ==============================================================================
* 命名空间：Quick.Models.Entity
* 类 名 称：GeneratedId
* 创 建 者：Qing
* 创建时间：2019-01-06 13:34:06
* CLR 版本：4.0.30319.42000
* 保存的文件名：GeneratedId
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Models.Entity
{
    public class GeneratedId
    {
        /// <summary>
        /// 在EF中， 如果主键为 integer 型，则数据库默认为自增长（效果等同于设置DatabaseGeneratedOption.Identity）,
        /// 如果主键是 GUID 类型，则要显式设置自增长；
        /// 如果不想自增长，可设置成 DatabaseGeneratedOption.None
        /// 它有三个枚举值：Computed、None、Identity：
        ///     Computed 表示这一列是计算所得
        ///     None 不做处理
        ///     Identity 自增
        /// DatabaseGeneratedOption.Computed： 在用 Code First 生成数据库的时候你可以在 byte 或 timestamp 列上使用 DatabaseGenerated Annotation，否则就应该在数据库存在的情况下使用因为如果数据库不存在，
        /// 此时 Code First 不知道为计算列（Computed Column）选择使用什么样的公式 
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]//不自动增长
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自动增长
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]//计算所得
        public Guid Id { get; set; }
    }
}
