using System.Web;

namespace QuickWeb.Models.UEditor
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class ConfigHandler : Handler
    {
        public ConfigHandler(HttpContext context) : base(context)
        {
        }

        public override string Process()
        {
            return WriteJson(UeditorConfig.Items);
        }
    }
}