using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quick.Common;
using Quick.Common.Net;
using Quick.IService;
using Quick.Models.Dto;
using Quick.Models.Entity.Enum;
using Quick.Models.Entity.Table;

namespace QuickWeb.Models.Hangfire
{
    public class HangfireBackJob : IHangfireBackJob
    {
        public IUserInfoService UserInfoService { get; set; }

        public void LoginRecord(UserInfoOutputDto userInfo, string ip, LoginType type)
        {
            LoginRecord record = new LoginRecord()
            {
                IP = ip,
                LoginTime = DateTime.Now,
                LoginType = type,
                PhysicAddress = "",
                Province = ip.GetProvince()
            };
            UserInfo u = UserInfoService.GetByUsername(userInfo.Username);
            u.LoginRecord.Add(record);
            UserInfoService.UpdateEntitySaved(u);
            string content = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "App_Data\\template\\login.html").Replace("{{name}}", u.Username).Replace("{{time}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Replace("{{ip}}", record.IP).Replace("{{address}}", record.PhysicAddress);
            CommonHelper.SendMail(CommonHelper.GetSettings("Title") + "账号登录通知", content, CommonHelper.GetSettings("ReceiveEmail"));
        }
    }
}