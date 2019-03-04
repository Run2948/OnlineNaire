/* ==============================================================================
* 命名空间：Quick.Service
* 类 名 称：BaseService
* 创 建 者：Qing
* 创建时间：2018-11-29 21:45:11
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseService
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



using Quick.Models.Common;
using SqlSugar;
using System;
using System.Linq;

namespace Quick.Service
{
    /// <summary>
    /// 业务层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseService<T>
    {
        #region SqlSugar 初始化
        /// <summary>
        /// 初始化ConnectionConfig对象
        /// </summary>
        private ConnectionConfig SqlSugarConnectionConfig => new ConnectionConfig()
        {
            ConnectionString = DbProvider.GetDbConStr(),
            DbType = DbType.SqlServer,
            InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
            IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
        };

        public BaseService()
        {
            DbClient = new SqlSugarClient(SqlSugarConnectionConfig);
#if DEBUG
            //调式代码 用来打印SQL 
            DbClient.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + DbClient.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
#endif
        }

        // 用来处理lbk_user表的常用操作
        //public SimpleClient<Student> StudentDb => new SimpleClient<Student>(DbClient);

        /// <summary>
        /// 用来处理T表的常用操作
        /// </summary>
        //public SimpleClient<T> SimpleDbClient => new SimpleClient<T>(DbClient);

        /// <summary>
        /// 用来处理事务多表查询和复杂的操作
        /// </summary>
        public SqlSugarClient DbClient;

        #endregion

        /// <summary>
        /// SqlSugar 用来处理事务多表查询和复杂的操作
        /// 详细操作见：http://www.codeisbug.com/Doc/8/1166
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient SugarClient()
        {
            return DbClient;
        }
    }
}
