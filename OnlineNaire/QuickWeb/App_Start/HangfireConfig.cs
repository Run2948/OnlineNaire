using Hangfire;
using Hangfire.Console;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QuickWeb
{
    public class HangfireConfig
    {
        public static void Register()
        {
            #region Hangfire配置

            //GlobalConfiguration.Configuration.UseMemoryStorage();
            GlobalConfiguration.Configuration.UseSqlServerStorage(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString).UseConsole();

            #region 实现类注册

            GlobalConfiguration.Configuration.UseAutofacActivator(AutofacConfig.Container);

            #endregion

            #region 服务启动

            Server = new BackgroundJobServer(new BackgroundJobServerOptions
            {
                ServerName = $"{Environment.MachineName}", //服务器名称
                SchedulePollingInterval = TimeSpan.FromSeconds(1),
                ServerCheckInterval = TimeSpan.FromSeconds(1),
                WorkerCount = Environment.ProcessorCount * 2,
                //Queues = new[] { "masuit" } //队列名
            });
            #endregion

            #endregion

            //RecurringJob.AddOrUpdate(() => Windows.ClearMemorySilent(), Cron.Hourly);//每小时清理系统内存
            //RecurringJob.AddOrUpdate(() => CheckLinks(), Cron.HourInterval(5));//每5h检查友链
            //RecurringJob.AddOrUpdate(() => EverydayJob(), Cron.Daily, TimeZoneInfo.Local);//每天的任务
            //RecurringJob.AddOrUpdate(() => FlushAddress(), Cron.Weekly(DayOfWeek.Sunday, 2), TimeZoneInfo.Local);//刷新没统计到的访客的信息
            //RecurringJob.AddOrUpdate(() => AggregateInterviews(), Cron.Hourly(30));//每半小时统计访客
        }

        public static BackgroundJobServer Server { get; set; }
    }
}