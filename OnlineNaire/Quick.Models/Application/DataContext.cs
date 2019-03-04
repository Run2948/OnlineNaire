/* ==============================================================================
* 命名空间：Quick.Models.Application
* 类 名 称：DataContext
* 创 建 者：Qing
* 创建时间：2019-01-05 18:09:27
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
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/



using EFSecondLevelCache;
using Quick.Models.Entity.Table;
using Quick.Models.Validation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masuit.Tools.Logging;
using Quick.Models.Common;
using static System.Data.Entity.Core.Objects.ObjectContext;

namespace Quick.Models.Application
{
    public partial class DataContext
    {
        public DataContext() :
            base(DbProvider.GetDataBaseProvider())
        {
            Database.CreateIfNotExists();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Quick.Models.Migrations.Configuration>());
#if DEBUG
            Database.Log = s =>
            {
                LogManager.Debug(typeof(Database), s);
            };
#endif
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //设置的表的名称是一个多元化的实体类型名称版本
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // 设置EntityFramework中decimal类型数据精度
            modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());
            modelBuilder.Entity<UserInfo>().HasMany(e => e.LoginRecord).WithRequired(e => e.UserInfo).WillCascadeOnDelete(true);
        }

        //重写 SaveChanges
        public int SaveChanges(bool invalidateCacheDependencies = true)
        {
            return SaveAllChanges(invalidateCacheDependencies);
        }

        public int SaveAllChanges(bool invalidateCacheDependencies = true)
        {
            var changedEntityNames = GetChangedEntityNames();
            var result = base.SaveChanges();
            if (invalidateCacheDependencies)
            {
                new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            }
            return result;
        }

        //修改、删除、添加数据时缓存失效
        private string[] GetChangedEntityNames()
        {
            return ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted).Select(x => GetObjectType(x.Entity.GetType()).FullName).Distinct().ToArray();
        }
    }
}
