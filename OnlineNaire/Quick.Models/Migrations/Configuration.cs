using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace Quick.Models.Migrations
{
    /// <summary>
    /// 数据上下文配置
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<Quick.Models.Application.DataContext>
    {
        public Configuration()
        {
            //开启自动迁移
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(Quick.Models.Application.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            try
            {
                #region 添加约束

                context.Database.ExecuteSqlCommand(@"ALTER TABLE [dbo].[Post] ADD DEFAULT getdate() FOR [PostDate];
                                                    ALTER TABLE [dbo].[Post] ADD DEFAULT getdate() FOR [ModifyDate];
                                                    ALTER TABLE [dbo].[Post] ADD DEFAULT 0 FOR [IsFixedTop];
                                                    ALTER TABLE [dbo].[Post] ADD DEFAULT 0 FOR [IsBanner];
                                                    ALTER TABLE [dbo].[SystemSetting] ADD DEFAULT 1 FOR [IsAvailable];
                                                    ALTER TABLE [dbo].[UserInfo] ADD DEFAULT 0 FOR [IsAdmin];
                                                    ");

                #endregion
            }
            catch (SqlException)
            {

            }
        }
    }
}
