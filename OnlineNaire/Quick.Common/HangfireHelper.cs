/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：HangfireHelper
* 创 建 者：Qing
* 创建时间：2018-11-30 11:19:08
* CLR 版本：4.0.30319.42000
* 保存的文件名：HangfireHelper
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



using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common
{
    public static class HangfireHelper
    {
        private static BackgroundJobClient Client { get; set; } = new BackgroundJobClient();

        public static string CreateJob(Type type, string method, string queue = "", params dynamic[] args)
        {
            var job = new Job(type, type.GetMethod(method), args);
            return string.IsNullOrEmpty(queue) ? Client.Create(job, new EnqueuedState()) : Client.Create(job, new EnqueuedState(queue));
        }
    }
}
