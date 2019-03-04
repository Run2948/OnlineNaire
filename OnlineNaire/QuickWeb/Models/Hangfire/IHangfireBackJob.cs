/* ==============================================================================
* 命名空间：QuickWeb.Models.Hangfire
* 类 名 称：IHangfireBackJob
* 创 建 者：Qing
* 创建时间：2018-11-30 11:33:40
* CLR 版本：4.0.30319.42000
* 保存的文件名：IHangfireBackJob
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



using Quick.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Models.Entity.Enum;

namespace QuickWeb.Models.Hangfire
{
    public interface IHangfireBackJob
    {
        void LoginRecord(UserInfoOutputDto userInfo, string ip, LoginType type);
    }
}
