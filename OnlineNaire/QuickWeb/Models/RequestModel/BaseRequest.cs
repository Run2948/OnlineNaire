
/* ==============================================================================
* 命名空间：QuickWeb.Models.RequestModel 
* 类 名 称：BaseRequest
* 创 建 者：Qing
* 创建时间：2019-03-04 16:39:14
* CLR 版本：4.0.30319.42000
* 保存的文件名：BaseRequest
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

using Quick.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickWeb.Models.RequestModel
{
    public class BaseRequest : PageRequest
    {
        public string NameSearch { get; set; }
    }

    public class MemberRequest : MemberLoginLogRequest
    {
        public string RelName { get; set; }
    }

    public class MemberLoginLogRequest : PageRequest
    {
        public string LoginName { get; set; }
    }

    public class RecyclingRequest : PageRequest
    {
        public long QuestionnaireId { get; set; }

        public int Source { get; set; }

        public string SendPhone { get; set; }

        public DateTime? StartConfirmTime { get; set; }

        public DateTime? EndConfirmTime { get; set; }
    }

}