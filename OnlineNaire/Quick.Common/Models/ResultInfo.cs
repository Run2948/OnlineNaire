/* ==============================================================================
* 命名空间：Quick.Models.Common
* 类 名 称：ResultInfo
* 创 建 者：Qing
* 创建时间：2018-11-30 10:18:18
* CLR 版本：4.0.30319.42000
* 保存的文件名：ResultInfo
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



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common.Models
{
    public class ResultInfo
    {
        public ResultInfo(int status, string msg, object data, string url)
        {
            Status = status;
            Msg = msg;
            Data = data;
            Url = url;
        }

        public ResultInfo(int status, string msg, object data)
        {
            Status = status;
            Msg = msg;
            Data = data;
        }

        public ResultInfo(int status, string msg, string url)
        {
            Status = status;
            Msg = msg;
            Url = url;
        }

        public ResultInfo(int status, string msg)
        {
            Status = status;
            Msg = msg;
        }

        public ResultInfo()
        {
        }

        /// <summary>
        /// 返回的状态码：ok: 1, error: 0, timeout: 2
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 返回的提示信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回的数据对象
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 需要跳转的地址
        /// </summary>
        public string Url { get; set; }
    }
}
