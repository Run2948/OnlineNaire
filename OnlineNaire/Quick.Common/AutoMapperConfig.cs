/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：AutoMapperConfig
* 创 建 者：Qing
* 创建时间：2018-11-29 23:32:54
* CLR 版本：4.0.30319.42000
* 保存的文件名：AutoMapperConfig
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



using AutoMapper;
using Quick.Models.Dto;
using Quick.Models.Entity.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<UserInfo, UserInfoInputDto>();
                m.CreateMap<UserInfoInputDto, UserInfo>();
                m.CreateMap<UserInfo, UserInfoOutputDto>();
                m.CreateMap<UserInfoOutputDto, UserInfo>();
                m.CreateMap<UserInfoInputDto, UserInfoOutputDto>();
                m.CreateMap<UserInfoOutputDto, UserInfoInputDto>();
            });
        }
    }
}
