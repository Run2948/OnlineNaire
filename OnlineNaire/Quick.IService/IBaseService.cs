/* ==============================================================================
* 命名空间：Quick.IService
* 类 名 称：IBaseService
* 创 建 者：Qing
* 创建时间：2018-11-29 22:36:21
* CLR 版本：4.0.30319.42000
* 保存的文件名：IBaseService
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



using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.IService
{
    public partial interface IBaseService<T>
    {
        /// <summary>
        /// SqlSugar 用来处理事务多表查询和复杂的操作
        /// 详细操作见：http://www.codeisbug.com/Doc/8/1166
        /// </summary>
        /// <returns></returns>
        SqlSugarClient SugarClient();
    }
}
